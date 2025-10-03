using System;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] TextMeshPro progressCount;
    private Vector2 currentTargetPos;
    private Vector2 direction;
    private bool reversedPathing;
    private int currentPathIdx;

    [SerializeField] private float baseMovementSpeed;
    private float currentMovementSpeed;

    private Vector2 offset;
    private Rigidbody2D rb;

    public Health Health { get; private set; }
    private bool destroyed;

    public float progress { get; private set; }

    private void Awake()
    {
        currentMovementSpeed = baseMovementSpeed;
        rb = GetComponent<Rigidbody2D>();
        Health = GetComponent<Health>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Health.OnHealthChanged += OnHealthChanged;
    }

    private void OnDestroy()
    {
        Health.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float health)
    {
        if (destroyed) return;
        if (health <= 0)
        {
            destroyed = true;
            gameObject.SetActive(false);
        }
    }

    public void Initialize(EnemyType enemyType = EnemyType.Basic)
    {
        destroyed = false;
        rb.position = transform.position;
        Health.Init();
        SetFirstDirection();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPath();
        float step = Mathf.Min(currentMovementSpeed * Time.deltaTime, GetTargetDistance());
        transform.position += (Vector3)direction * step;
    }

    public void FixedUpdate()
    {
        
    }

    public void SetFirstDirection()
    {
        offset = (Vector2)transform.position - PathManager.instance.GetFirstPoint();
        currentTargetPos = PathManager.instance.GetFirstPath() + offset;
        currentPathIdx = 1;
        direction = (currentTargetPos - (Vector2)transform.position).normalized;
    }

    public void SetDirection()
    {
        //currentPathIdx = PathManager.instance.GetNextPathIdx(currentPathIdx);
        Vector2 currentPathPoint = PathManager.instance.Paths[currentPathIdx];
        offset = (Vector2)transform.position - currentPathPoint;
        currentTargetPos = currentPathPoint;
        direction = (currentTargetPos - (Vector2)transform.position).normalized;
    }

    public void CheckPath()
    {
        float currentPathDistance = PathManager.instance.PathsDistance[currentPathIdx - 1];
        progress = currentPathDistance - GetTargetDistance();
        progressCount.text = ((progress / PathManager.instance.PathLength) * 100).ToString("F1");
        // Change Path
        if (progress > currentPathDistance - 0.01f)
        {
            if (IsLastPath())
            {
                gameObject.SetActive(false);
                PlayerManager.instance.ReduceLife();
                return;
            }
            currentPathIdx++;
            SetDirection();
            return;
        }

        // Check Backlog path
        if (currentPathIdx < 2) return; 
        float progressDifference = currentPathDistance - progress;
        float previousPathPoint = PathManager.instance.PathsDistance[currentPathIdx - 2];
        if (progressDifference > currentPathDistance - previousPathPoint)
        {
            currentPathIdx--;
            SetDirection();
        }
    }

    public void CheckPathReverse()
    {
        // to be implemented, for knockbacks, or if the enemy gets displaced backward
    }

    public bool IsLastPath()
    {
        return currentPathIdx == PathManager.instance.Paths.Count - 1;
    }

    public bool TargetReached()
    {
        return (currentTargetPos - (Vector2)transform.position).sqrMagnitude < 0.01;
    }

    public float GetTargetDistance()
    {
        return (currentTargetPos - (Vector2)transform.position).magnitude;
    }

}

public enum EnemyType
{
    Basic,
}