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
