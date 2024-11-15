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

    GameObject NewWeaponMenu;

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

        LevelUpMenu = GameObject.FindGameObjectWithTag("Level Up Menu");
        pauseScript = gameObject.GetComponent<Pause_System>();
    }

    private void Start()
    {
        
        HideLevelUpMenu();

    }

    public void ShowLevelUpMenu()
    {
        LevelUpMenu.SetActive(true);
        pauseScript.PauseGame();
    }

    public void HideLevelUpMenu()
    {
        LevelUpMenu.SetActive(false);
        pauseScript.ResumeGame();
    }

    public void ShowMenu(GameObject menuParent)
    {

    }

    public void ToggleNewWeaponMenu()
    {
        //if not active, pause game and show the menu
        if (!NewWeaponMenu.activeSelf)
        {
            NewWeaponMenu.SetActive(true);
            pauseScript.PauseGame();
        }
        //if active, close menu and resume game
        else
        {
            NewWeaponMenu.SetActive(false);
            pauseScript.ResumeGame();
        }
    }

}
