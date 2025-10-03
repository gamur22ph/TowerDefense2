using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildManager : MonoBehaviour
{
    public Action OnBuildingChanged;

    public BuildingData testBuilding;
    public GameObject buildingPrefab;
    public static BuildManager instance;

    public BuildingData currentBuilding;
    public BuildCursor buildCursor;

    private bool buildPressed;
    private Vector3 buildMousePos;

    private void Awake()
    {
        instance = this;
        currentBuilding = testBuilding;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.PlayerInput.actions.FindAction("Cancel").performed += OnCancelPressed;
    }

    private void OnDestroy()
    {
        if (InputManager.PlayerInput == null) return;
        InputManager.PlayerInput.actions.FindAction("Cancel").performed -= OnCancelPressed;
    }

    private void OnCancelPressed(InputAction.CallbackContext context)
    {
        if (context.action.WasPressedThisFrame())
        {
            if (currentBuilding != null)
            {
                SetCurrentBuilding(null);
                OnBuildingChanged?.Invoke();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBuilding == null) return;
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
                Building newBuilding = Instantiate(buildingPrefab, mousePos, Quaternion.identity).GetComponent<Building>();
                newBuilding.SetID(GUID.Generate().ToString());
                PlayerManager.instance.RegisterBuilding(newBuilding);
                Debug.Log("Tower " + newBuilding.id + " is registered");
            }
        }
        
    }

    private void FixedUpdate()
    {

    }

    public void SetCurrentBuilding(BuildingData building)
    {
        if (building == null)
        {
            buildCursor.gameObject.SetActive(false);
            currentBuilding = null;
            return;
        }
        currentBuilding = building;
        buildCursor.GetComponent<SpriteRenderer>().sprite = building.buildingSprite;
    }
}
