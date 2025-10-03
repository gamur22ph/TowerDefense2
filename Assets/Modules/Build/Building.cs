using System;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string id { get; private set; }

    public Action Fire;

    [SerializeField] private BuildingData buildingData;

    [SerializeField] private GameObject projectile;
    
    public enum TargetPriority
    {
        First,
        Last,
        Close,
        Strong
    }
    [SerializeField] private TargetPriority currentTargetPriority;
    public float fireRateTimer { get; private set; }
    public float fireRateTime;

    [SerializeField] private float damage;
    [SerializeField] private float baseRange;
    private float currentRange;
    private Collider2D[] targets = new Collider2D[30];
    [SerializeField] private ContactFilter2D enemyFilter;
    private int targetHits;
    private Collider2D currentTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Fire = SpawnNormalBullet;
        currentRange = baseRange;
    }

    // Update is called once per frame
    void Update()
    {
        fireRateTimer -= Time.deltaTime;
        DetectTargets();
        HandleAttack();
    }

    private void SpawnNormalBullet()
    {
        Projectile newProjectile = ObjectPool.Instance.GetObject(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
        newProjectile.Init(gameObject, currentTarget.gameObject, damage, 12);
        newProjectile.SetDecayTime(2);
    }

    public void HandleAttack()
    {
        if (HasTarget())
        {
            switch (currentTargetPriority)
            {
                case TargetPriority.First:
                    currentTarget = Helper.Targeting.GetFirst(transform.position, targets, targetHits);
                    break;
                case TargetPriority.Last:
                    currentTarget = Helper.Targeting.GetLast(transform.position, targets, targetHits);
                    break;
                case TargetPriority.Close:
                    currentTarget = Helper.Targeting.GetClosest(transform.position, targets, targetHits);
                    break;
                case TargetPriority.Strong:
                    currentTarget = Helper.Targeting.GetStrongest(transform.position, targets, targetHits);
                    break;
            }
            
            if (fireRateTimer <= 0)
            {
                Fire();
                fireRateTimer = fireRateTime;
            }
        }
    }
    
    public void DetectTargets()
    {
        targetHits = Physics2D.OverlapCircle(transform.position, currentRange, enemyFilter, targets);
    }

    public bool HasTarget()
    {
        return targetHits > 0;
    }

    public void SetID(string id)
    {
        this.id = id;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, currentRange);
    }
}
