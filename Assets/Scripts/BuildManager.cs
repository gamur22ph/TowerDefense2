using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    public GameObject testBuilding;
    public static BuildManager instance;

    public BuildingData currentBuilding;
    public BuildCursor buildCursor;

    private bool buildPressed;
    private Vector3 buildMousePos;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;
        buildCursor.transform.position = mousePos;

        bool canBuild = buildCursor.CheckIfBuildable();


        if (canBuild)
        {
            buildCursor.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            buildCursor.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (InputManager.PlayerInput.actions.FindAction("Interact").IsPressed())
        {
            if (canBuild)
            {
                GameObject testObject = Instantiate(testBuilding, mousePos, Quaternion.identity);
            }
        }
        
    }

    private void FixedUpdate()
    {

    }

    public void SetCurrentBuilding(BuildingData building)
    {
        currentBuilding = building;
        buildCursor.GetComponent<SpriteRenderer>().sprite = building.buildingSprite;
    }
}
