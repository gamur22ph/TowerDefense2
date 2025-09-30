using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 currentTargetPos;
    private Vector2 direction;
    private bool reversedPathing;
    private int currentPathIdx;

    [SerializeField] private float baseMovementSpeed;
    private float currentMovementSpeed;

    private Vector2 offset;
    private Rigidbody2D rb;

    

    private void Awake()
    {
        currentMovementSpeed = baseMovementSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Initialize(EnemyType enemyType = EnemyType.Basic)
    {
        SetFirstDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetReached())
        {
            if (IsLastPath())
            {
                gameObject.SetActive(false);
                PlayerManager.instance.ReduceLife();
            }
            SetNextDirection();
        }
        else
        {
            float step = Mathf.Min(currentMovementSpeed * Time.deltaTime, GetTargetDistance());
            transform.position += (Vector3)direction * step;
        }
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

    public void SetNextDirection()
    {
        currentPathIdx = PathManager.instance.GetNextPathIdx(currentPathIdx);
        Vector2 currentPathPoint = PathManager.instance.Paths[currentPathIdx];
        offset = (Vector2)transform.position - currentPathPoint;
        currentTargetPos = currentPathPoint;
        direction = (currentTargetPos - (Vector2)transform.position).normalized;
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