using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralFuel_System : MonoBehaviour
{

    public static MineralFuel_System instance;

    bool isRunning = false;

    float consumptionRate = 1;

    float fuelConsumeCooldown;
    float fuelConsumeDelay;

    float commonRateIncrease = .1f;
    float uncommonRateIncrease = .2f;
    float rareRateIncrease = .4f;
    float legendaryRateIncrease = .8f;

    // Start is called before the first frame update
    void Start()
    {
        //singleton code
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        UpdateConsumptionDelay();

    }

    private void FixedUpdate()
    {
        if (isRunning)
        {
            fuelConsumeCooldown -= Time.deltaTime;

            //on a cooldown, subtract one mineral from the count
            //this uses a timer so that the mineralCount can be a uint
            if (fuelConsumeCooldown <= 0 )
            {
                Currencies_System.instance.mineralCount--;
                fuelConsumeCooldown = fuelConsumeDelay;
            }
        }
    }

    [ContextMenu("Start Consuming")]
    void StartConsumingFuel()
    {
        isRunning = true;
    }

    [ContextMenu("Stop Consuming")]
    void StopConsumingFuel()
    {
        isRunning = false;
    }

    void UpdateConsumptionDelay()
    {
        fuelConsumeDelay = 1 / consumptionRate;
    }

    public void RaiseConsumptionRate(UpgradeTier upgradeTier)
    {
        switch (upgradeTier)
        {
            case UpgradeTier.Common:
                consumptionRate += commonRateIncrease;
                break;

            case UpgradeTier.Uncommon:
                consumptionRate += uncommonRateIncrease;
                break;

            case UpgradeTier.Rare:
                consumptionRate += rareRateIncrease;
                break;

            case UpgradeTier.Legendary:
                consumptionRate += legendaryRateIncrease;
                break;
        }

        UpdateConsumptionDelay();

    }

}
