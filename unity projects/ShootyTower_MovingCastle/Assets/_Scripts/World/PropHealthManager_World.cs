using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropHealthManager_World : MonoBehaviour
{

    public static PropHealthManager_World instance;

    [SerializeField] public List<float> propHealthList;

    // Start is called before the first frame update
    void Start()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Returns array of all objects of type PropHealth_World
    /// </summary>
    /// <returns></returns>
    PropHealth_World[] GatherProps()
    {
        PropHealth_World[] propList = FindObjectsByType<PropHealth_World>(FindObjectsSortMode.InstanceID);

        return propList;
    }

    [ContextMenu("Update Prop Health List")]
    public void UpdateHealthList()
    {
        propHealthList.Clear();
        PropHealth_World[] propList = GatherProps();

        foreach (PropHealth_World prop in propList)
        {
            propHealthList.Add(prop.currentHealth);
        }
    }

    /// <summary>
    /// overrides the prop health list with a new health list
    /// </summary>
    /// <param name="newHealthList"></param>
    public void SetHealthList(List<float> newHealthList)
    {
        propHealthList.Clear();
        propHealthList = newHealthList;
    }

    [ContextMenu("Update Health On Props")]
    public void UpdateHealthOnProps()
    {
        PropHealth_World[] propList = GatherProps();
        for (int i = 0; i < propHealthList.Count; i++)
        {
            propList[i].SetCurrentHealth(propHealthList[i]);
        }
    }

}
