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
    public uint currentLevel = 1; 
    public float lvlUpXpRequirement = 5;
    public float currentXP;

    public static Leveling_Player instance;

    private void Start()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
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
