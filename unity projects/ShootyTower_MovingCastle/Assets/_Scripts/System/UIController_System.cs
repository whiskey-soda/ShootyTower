using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pause_System))]

public class UIController_System : MonoBehaviour
{
    public static UIController_System Instance;

    [Header("DEBUG")]
    [SerializeField] GameObject LevelUpMenu;
    Pause_System pauseScript;

    private void Awake()
    {
        pauseScript = gameObject.GetComponent<Pause_System>();
    }

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
        pauseScript.PauseGame();
    }

    public void HideLevelUpMenu()
    {
        LevelUpMenu.SetActive(false);
        pauseScript.ResumeGame();
    }
}
