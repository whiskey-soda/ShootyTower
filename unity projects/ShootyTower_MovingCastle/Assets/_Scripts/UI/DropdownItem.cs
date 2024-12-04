using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownItem : MonoBehaviour
{
    Toggle toggle;

    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        //sets this option as active or inactive based on the highest height level

        //if all layers are armed, all layers can recieve new weps
        if (AddWeaponOptionManager_System.instance.allLayersArmed)
        {
            Activate();
        }
        //if all layers are NOT armed, only the next layer can recieve a weapon
        else
        {

            // +2 is added because first item in the list is a template item and second is SELECT LAYER
            if (transform.GetSiblingIndex() == (int)AddWeaponOptionManager_System.instance.heightLevelToArmNext + 2)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }
        
    }

    public void Activate()
    {
        toggle.interactable = true;
    }

    public void Deactivate()
    {
        toggle.interactable = false;
    }


}
