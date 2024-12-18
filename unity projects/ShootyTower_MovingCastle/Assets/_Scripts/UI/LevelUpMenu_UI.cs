using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class LevelUpMenu_UI : MonoBehaviour
{

    GameObject newWeaponButton;

    [Header("CONFIG")]
    [SerializeField] List<uint>newWeaponLevels = new List<uint>();

    [SerializeField] float newWeaponChance = .2f;

    private void Awake()
    {

        newWeaponButton = GameObject.FindGameObjectWithTag("New Weapon Button");
    }


    public void InitMenu()
    {
        //check if player has reached a level designated as a new weapon level
        bool showWeaponMenu = false;

        foreach (uint level in newWeaponLevels)
        {
            if (Leveling_Player.instance.currentLevel == level)
            {
                showWeaponMenu = true;
            }
        }

        if (showWeaponMenu)
        {

            UIController_System.Instance.ToggleNewWeaponMenu();
        }
        else
        {
            UIController_System.Instance.ShowUpgradeMenu();

            //random chance to show new weapon button
            float newWeaponRollResult = Random.Range(0f, 1f);
            if (newWeaponRollResult <= newWeaponChance)
            {
                newWeaponButton.SetActive(true);
            }
            else
            {
                newWeaponButton.SetActive(false);
            }
        }
    }
}
