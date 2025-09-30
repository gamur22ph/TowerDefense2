using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public static PlayerInput PlayerInput;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            PlayerInput = GetComponent<PlayerInput>();
        }
        else 
        {
            Destroy(gameObject);       
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
