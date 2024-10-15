using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralDeposit_World : PropHealth_World
{
    
    float crystalDropChance;
    float dustDropChance;

    protected override void Start()
    {
        base.Start();
        crystalDropChance = 1 / (1.5f * maxHealth);
        dustDropChance = .6f / (1.5f * maxHealth);
    }

    public override void TakeDamage(float damageVal)
    {
        //give drops for each point of damage taken by the mineral deposit
        for (int i=0; i<(int)damageVal; i++)
        {
            GiveMineral();

            if (TryGiveDrop(crystalDropChance)) { GiveCrystal(); }
            if (TryGiveDrop(dustDropChance)) { GiveDust(); }
        }

        base.TakeDamage(damageVal);
    }

    void GiveMineral()
    {
        Currencies_System.instance.mineralCount++;
    }

    void GiveCrystal()
    {
        Currencies_System.instance.crystalCount++;
    }

    void GiveDust()
    {
        Currencies_System.instance.dustCount++;
    }
       
    /// <summary>
    /// rolls a random float from 0-1. if float is below the drop chance, return true. else return false.
    /// </summary>
    /// <param name="dropChance">float between 0 and 1</param>
    /// <returns></returns>
    bool TryGiveDrop(float dropChance)
    {
        bool isSuccess = false;
        float rollResult = Random.Range(0f, 1f);

        if (rollResult <= dropChance)
        {
            isSuccess = true;
        }

        return isSuccess;
    }

}
