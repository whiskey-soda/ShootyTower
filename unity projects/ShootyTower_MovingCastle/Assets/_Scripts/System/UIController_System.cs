using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pause_System))]

public class UIController_System : MonoBehaviour
{
    public static UIController_System Instance;

    [Header("DEBUG")]
    [SerializeField] GameObject LevelUpMenu;
    [SerializeField] GameObject upgradeMenu;
    [SerializeField] GameObject weaponMenu;
    Pause_System pauseScript;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //the entire menu that comes up when u level up
        LevelUpMenu = GameObject.FindGameObjectWithTag("Level Up Menu");
        pauseScript = gameObject.GetComponent<Pause_System>();

        // the menu child that contains upgrade options
        upgradeMenu = GameObject.FindGameObjectWithTag("Upgrade Menu");

        // the menu child that contains weapon options
        weaponMenu = GameObject.FindGameObjectWithTag("New Weapon Menu");
    }

    private void Start()
    {
        
        HideLevelUpMenu();

    }

    public void ShowLevelUpMenu()
    {
        LevelUpMenu.SetActive(true);
        LevelUpMenu.GetComponent<LevelUpMenu_UI>().InitMenu();
        pauseScript.PauseGame();
    }

    public void HideLevelUpMenu()
    {
        LevelUpMenu.SetActive(false);
        pauseScript.ResumeGame();
    }

    public void ShowUpgradeMenu()
    {
        upgradeMenu.SetActive(true);
    }

    public void HideUpgradeMenu()
    {
        upgradeMenu.SetActive(false);
    }

    public void ToggleNewWeaponMenu()
    {
        //if not active, pause game and show the menu
        if (!weaponMenu.activeSelf)
        {
            weaponMenu.SetActive(true);
            pauseScript.PauseGame();
        }
        //if active, close menu and resume game
        else
        {
            weaponMenu.SetActive(false);
            pauseScript.ResumeGame();
        }
    }

}
