using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionMenu_UI : MonoBehaviour
{

    [Header("CONFIG")]
    [SerializeField] GameObject regionPanelPrefab;

    public static RegionMenu_UI instance;

    private void Awake()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadRegionMenu();
    }

    /// <summary>
    /// loads regions from the json file and displays them in panels
    /// </summary>
    void LoadRegionMenu()
    {
        RegionManager_System.instance.LoadRegionsFromJSON();
        DisplayRegionPanels();
    }

    [ContextMenu("Display Region Panels")]
    public void DisplayRegionPanels()
    {
        foreach (RegionData_World regionData in RegionManager_System.instance.regions)
        {
            GameObject regionPanel = Instantiate(regionPanelPrefab, transform);
            regionPanel.GetComponent<RegionPanel_UI>().DisplayRegionInfo(regionData);
        }
    }

}
