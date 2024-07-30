using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Leveling_Player : MonoBehaviour
{

    public UnityEvent LevelUp;

    [Header("CONFIG")]
    [SerializeField] float xpRequirementIncreasePerLevel = 10;

    [Header("DEBUG")]
    [SerializeField] uint currentLevel = 1;
    [SerializeField] float lvlUpXpRequirement = 5;
    [SerializeField] float currentXP;

    public static Leveling_Player Instance;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentXP >= lvlUpXpRequirement)
        {
            LevelUp.Invoke();
            RaiseLevel();
        }
    }

    void RaiseLevel()
    {
        currentLevel++;
        lvlUpXpRequirement += xpRequirementIncreasePerLevel;
        currentXP = 0;
    }

    public void AddXp(float value)
    {
        currentXP += value;
    }

}
