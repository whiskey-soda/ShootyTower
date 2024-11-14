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
    public void LoadRegionMenu()
    {
        ClearRegionMenu();
        RegionManager_System.instance.LoadRegionsFromJSON();
        DisplayRegionPanels();
    }

    /// <summary>
    /// deletes all panels in the region menu
    /// </summary>
    private void ClearRegionMenu()
    {
        RegionPanel_UI[] existingPanels = GetComponentsInChildren<RegionPanel_UI>();
        foreach (RegionPanel_UI panel in existingPanels)
        {
            Destroy(panel.gameObject);
        }
    }

    [ContextMenu("Display Region Panels")]
    void DisplayRegionPanels()
    {
        foreach (RegionData_World regionData in RegionManager_System.instance.regions)
        {
            GameObject regionPanel = Instantiate(regionPanelPrefab, transform);
            regionPanel.GetComponent<RegionPanel_UI>().DisplayRegionInfo(regionData);
        }
    }

}
