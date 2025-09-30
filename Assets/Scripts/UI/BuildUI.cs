using System.Collections.Generic;
using UnityEditor.Rendering.BuiltIn.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour
{
    [SerializeField] private GameObject grid;
    [SerializeField] private BuildingData[] buildingList; 

    public List<BuildItem> buildItems = new List<BuildItem>();

    private void Awake()
    {
        foreach (Transform item in grid.transform)
        {
            buildItems.Add(new BuildItem(item.gameObject));
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

public class BuildItem
{
    public BuildingData data { get; private set; }
    public GameObject go { get; private set; }

    public Image buildingImage { get; private set; }
    public Button button { get; private set; }

    public BuildItem (GameObject go)
    {
        this.go = go;
        button = go.GetComponent<Button>();
        buildingImage = go.transform.GetChild(0).GetComponent<Image>();
    }

    public void SetData(BuildingData data)
    {
        this.data = data;
        buildingImage.overrideSprite = data.icon64x64;
    }
}
