using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController_System : MonoBehaviour
{
    public static UIController_System Instance;

    [Header("DEBUG")]
    [SerializeField] GameObject LevelUpMenu;

    private void Start()
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
        HideLevelUpMenu();

    }

    public void ShowLevelUpMenu()
    {
        LevelUpMenu.SetActive(true);
    }

    void HideLevelUpMenu()
    {
        LevelUpMenu.SetActive(false);
    }
}
