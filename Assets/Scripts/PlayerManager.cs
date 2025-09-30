using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public delegate void OnDamageDelegate();
    public event OnDamageDelegate OnDamage;
    public delegate void OnLifeChangedDelegate(float currentLives);
    public event OnLifeChangedDelegate OnLifeChanged;

    public static PlayerManager instance;
    [SerializeField] private int baseLives;

    public int CurrentLives { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentLives = baseLives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReduceLife(int damage = 1)
    {
        CurrentLives = Mathf.Clamp(CurrentLives - damage, 0, int.MaxValue);
        OnDamage?.Invoke();
        OnLifeChanged?.Invoke(CurrentLives);
    }
}
