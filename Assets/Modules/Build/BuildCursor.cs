using UnityEngine;

public class BuildCursor : MonoBehaviour
{
    [SerializeField] private float radius = 0.5f;
    private Collider2D[] pathsDetected = new Collider2D[1];
    [SerializeField] private ContactFilter2D unbuildableFilter;
    public bool Buildable {  get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Path"))
        {
            Buildable = false;
            Debug.Log("Cursor Set to Unbuildable");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Path"))
        {
            Buildable = true;
            Debug.Log("Cursor Set to Buildable");
        }
    }

    private void Awake()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        
    }

    public bool CheckIfBuildable()
    {
        int numHits = Physics2D.OverlapCircle(transform.position, radius, unbuildableFilter, pathsDetected);
        return numHits == 0;
    }
}
