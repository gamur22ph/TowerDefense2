using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage;
    private float projectileSpeed;
    private Vector3 direction;
    public float decayTimer { get; private set; }
    public GameObject sender { get; private set; }
    public GameObject target { get; private set; }

    // Hits
    [SerializeField] private int maxHits;
    private int hitCount;
    private HitInfo projectileHitInfo;

    private HashSet<Collider2D> targetsHit = new HashSet<Collider2D>();
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hitCount >= maxHits)
        {
            gameObject.SetActive(false);
            return;
        }

        if (other.tag == "Enemy" && !targetsHit.Contains(other))
        {
            other.GetComponent<IDamageable>().Damage(projectileHitInfo);
            targetsHit.Add(other);
            hitCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        decayTimer -= Time.deltaTime;
        if (decayTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Init(GameObject sender, GameObject target, float damage, float projectileSpeed) 
    {
        this.sender = sender;
        this.damage = damage;
        this.projectileSpeed = projectileSpeed;
        this.target = target;
        rb.position = transform.position;
        direction = target.transform.position - transform.position;
        direction.Normalize();
        projectileHitInfo = new HitInfo(sender, damage);
        hitCount = 0;
        targetsHit.Clear();
    }

    public void SetDecayTime(float decayTime)
    {
        decayTimer = decayTime;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = projectileSpeed * direction;
    }
}
