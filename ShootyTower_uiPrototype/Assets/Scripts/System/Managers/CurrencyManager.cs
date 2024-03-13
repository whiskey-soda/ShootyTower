using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public float currentManaCount;
    public float manaPerKill;
    public float nextReward;
    public float rewardReqIncrease = 200;
    public static CurrencyManager singleton;
    public UpgradeManager upgradeManagerScript;
    public TextMeshProUGUI manaCountText;
    public TextMeshProUGUI nextRewardText;
    public List<GameObject> gearList;
    public float maxGearRotationSpeed = 600;
    public float rotationSpeedPercentage = 0;
    public float currentGearRotationSpeed = 0;
    public float previousUpgradeManaReq;
    public bool gearsDecellerating = false;
    public float decelleration = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        //singleton code
        if (singleton == null) { singleton = this; }
        else { Destroy(gameObject); }
    }

    // Update is called once per frame
    void Update()
    {
        if (gearsDecellerating)
        {
            currentGearRotationSpeed -= 1;
            if (currentGearRotationSpeed <= 30) 
            { 
                gearsDecellerating = false; 
            }
        }
        else
        {
            rotationSpeedPercentage = (currentManaCount - previousUpgradeManaReq) / (nextReward - previousUpgradeManaReq);
            currentGearRotationSpeed = Mathf.Max(rotationSpeedPercentage * maxGearRotationSpeed, 30);
        }

        manaCountText.text = "Mana: " + currentManaCount;
        nextRewardText.text = "Next Upgrade: " + nextReward + " Mana";
        foreach (GameObject gear in gearList)
        {
            gear.transform.Rotate(new Vector3(0, 0, currentGearRotationSpeed * Time.deltaTime));
        }

        if (currentManaCount >= nextReward)
        {
            upgradeManagerScript.runUpgradeProcess();
            previousUpgradeManaReq = nextReward;
            gearsDecellerating = true;
            nextReward = currentManaCount + rewardReqIncrease;
            rewardReqIncrease *= 1.15f;
            rewardReqIncrease = Mathf.Round(rewardReqIncrease);
            while (rewardReqIncrease % 50 != 0)
            {
                rewardReqIncrease--;
            }
        }

    }

    public void grantManaFromKill()
    {
        currentManaCount += manaPerKill;
    }
}
