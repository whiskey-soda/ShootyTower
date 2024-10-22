using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HUD_UI : MonoBehaviour
{

    public static HUD_UI instance;

    [SerializeField] TextMeshProUGUI textHud;
    

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
        textHud = GetComponentInChildren<TextMeshProUGUI>();
        
    }

    private void FixedUpdate()
    {
        RefreshHudText();
    }

    private void RefreshHudText()
    {
        textHud.SetText(
                    $"Health: {Health_Player.instance.currentHealth}\n" +
                    $"Level: {Leveling_Player.instance.currentLevel}\n" +
                    $"XP: {Leveling_Player.instance.currentXP} / {Leveling_Player.instance.lvlUpXpRequirement}\n" +
                    $"Minerals: {Currencies_System.instance.mineralCount}\n" +
                    $"Crystals: {Currencies_System.instance.crystalCount}\n" +
                    $"Crystal Dust: {Currencies_System.instance.dustCount}\n" +
                    $"\n" +
                    $"Wave: {SpawnDirector_System.instance.currentWaveNum}\n" +
                    $"Difficulty: {SpawnDirector_System.instance.difficulty}");
    }
}
