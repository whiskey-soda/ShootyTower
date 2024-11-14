using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegionPanel_UI : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] TextMeshProUGUI worldSizeText;

    [Header("DEBUG")]
    //this is here so that the panel's buttons can load and delete this region
    RegionData_World myRegion;

    public void DisplayRegionInfo(RegionData_World regionData)
    {
        difficultyText.text = $"Difficulty: {   (regionData.spawnsPerSec + regionData.healthMultiplier) / 2 }";
        worldSizeText.text = $"Region Size: {  regionData.worldSize  }";

        myRegion = regionData;
    }

    /// <summary>
    /// deletes this region from the regions list and deletes this region panel
    /// </summary>
    public void DeleteRegion()
    {
        RegionManager_System.instance.regions.Remove(myRegion);
        RegionManager_System.instance.SaveRegionsToJSON();
        RegionManager_System.instance.LoadRegionsFromJSON();
        RegionMenu_UI.instance.LoadRegionMenu();
        Destroy(gameObject);
    }

    public void LoadRegion()
    {
        RegionManager_System.instance.LoadRegion(myRegion);
    }

}
