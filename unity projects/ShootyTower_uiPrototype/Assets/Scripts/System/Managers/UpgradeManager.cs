using System.Collections.Generic;
using TMPro;
//using UnityEditor.Animations;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<SO_TowerUpgrade> upgradeList = new List<SO_TowerUpgrade>();
    public float towerTop = 1.2f;
    public GameObject sniperPrefab;
    public GameObject tackPrefab;
    public TowerGM towerHealthScript;

    public static UpgradeManager singleton;
    public GameObject leftUpgradePanel;
    public TextMeshProUGUI leftUpgradeName;
    public GameObject rightUpgradePanel;
    public TextMeshProUGUI rightUpgradeName;
    public Animator panelAnimator;
    public List<UIRotatingGear> gearsList;

    public SO_TowerUpgrade upgrade1;
    public SO_TowerUpgrade upgrade2;

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
        
    }

    public void runUpgradeProcess()
    {
        upgrade1 = chooseRandomUpgrade();
        upgrade2 = chooseRandomUpgrade();

        panelAnimator.SetTrigger("Appear");
        stopGearRotation();

        leftUpgradeName.text = upgrade1.upgradeName;
        rightUpgradeName.text = upgrade2.upgradeName;

        
    }

    SO_TowerUpgrade chooseRandomUpgrade()
    {
        int randomIndex = Random.Range(0, upgradeList.Count);
        return upgradeList[randomIndex];
    }

    public void selectUpgrade(int selectedUpgrade)
    {
        if (selectedUpgrade == 1)
        {
            applyUpgrade(upgrade1);
        }
        else
        {
            applyUpgrade(upgrade2);
        }

        panelAnimator.SetTrigger("Disappear");
        startGearRotation();
    }

    public void applyUpgrade(SO_TowerUpgrade towerUpgrade)
    {
        
        switch(towerUpgrade.towerType)
        {
            //apply upgrades for each tower type because i did not use consistent names
            //this is AWFUL- need to fix eventually once the upgrade system is more generalizeable

            //upgrade all tackshooters
            case SO_TowerUpgrade.towerTypeEnum.TackShooter:

                //get list of all applicable towers
                TackShooter[] allTackShooterScripts = FindObjectsOfType<TackShooter>();

                //upgrade stats for each tower in array
                foreach (TackShooter towerScript in allTackShooterScripts)
                {
                    switch (towerUpgrade.stat)
                    {
                        case SO_TowerUpgrade.towerStatsEnum.fireRate:
                            towerScript.shotsPerSecond += towerUpgrade.buffValue;
                            towerScript.calculateFiringDelay();
                            break;
                        case SO_TowerUpgrade.towerStatsEnum.damage:
                            towerScript.projectileDmg += towerUpgrade.buffValue;
                            break;
                        case SO_TowerUpgrade.towerStatsEnum.tack_range:
                            towerScript.tackLifetime += towerUpgrade.buffValue;
                            break;
                        case SO_TowerUpgrade.towerStatsEnum.tack_spikeCount:
                            towerScript.numOfSpikes += (uint)towerUpgrade.buffValue;
                            break;
                        case SO_TowerUpgrade.towerStatsEnum.bulletSpeed:
                            towerScript.projectileSpeed += towerUpgrade.buffValue;
                            break;
                    }
                }
                break;

            //upgrade all snipers
            case SO_TowerUpgrade.towerTypeEnum.Sniper:

                //get list of all applicable towers
                Sniper[] allSniperScripts = FindObjectsOfType<Sniper>();

                //upgrade stats for each tower in array
                foreach (Sniper towerScript in allSniperScripts)
                {
                    switch (towerUpgrade.stat)
                    {
                        case SO_TowerUpgrade.towerStatsEnum.fireRate:
                            towerScript.shotsPerSecond += towerUpgrade.buffValue;
                            towerScript.calculateFiringDelay();
                            break;
                        case SO_TowerUpgrade.towerStatsEnum.damage:
                            towerScript.projectileDmg += towerUpgrade.buffValue;
                            break;
                        case SO_TowerUpgrade.towerStatsEnum.bulletSpeed:
                            towerScript.projectileSpeed += towerUpgrade.buffValue;
                            break;
                    }
                }
                break;

            //upgrade all stabbers
            case SO_TowerUpgrade.towerTypeEnum.Stabber:

                //get list of all applicable towers
                Spikes[] allStabberScripts = FindObjectsOfType<Spikes>();

                //upgrade stats for each tower in array
                foreach (Spikes towerScript in allStabberScripts)
                {
                    switch (towerUpgrade.stat)
                    {
                        case SO_TowerUpgrade.towerStatsEnum.damage:
                            towerScript.spikeDPS += towerUpgrade.buffValue;
                            towerScript.repairSpikes();
                            break;
                        case SO_TowerUpgrade.towerStatsEnum.stabber_durability:
                            towerScript.maxDurabilityLevel += towerUpgrade.buffValue;
                            towerScript.repairSpikes();
                            break;
                        case SO_TowerUpgrade.towerStatsEnum.stabber_repair:
                            towerScript.currentDurabilityLevel = towerScript.maxDurabilityLevel;
                            break;
                    }
                }

                break;
            case SO_TowerUpgrade.towerTypeEnum.Other:

                if (towerUpgrade.stat == SO_TowerUpgrade.towerStatsEnum.additional_tack)
                {
                    Instantiate(tackPrefab, new Vector3(0, .4f, 0), Quaternion.identity);
                }
                else if (towerUpgrade.stat == SO_TowerUpgrade.towerStatsEnum.additional_sniper)
                {
                    Instantiate(sniperPrefab, new Vector3(0, towerTop, 0), Quaternion.identity);
                    towerTop += .4f;
                }
                else if (towerUpgrade.stat == SO_TowerUpgrade.towerStatsEnum.full_heal)
                {
                    towerHealthScript.towerCurrentHealth = towerHealthScript.towerMaxHealth;
                }
                break;
        }

        

    }

    public void startGearRotation()
    {
        foreach (UIRotatingGear gear in gearsList)
        {
            gear.startRotating();
        }
    }

    public void stopGearRotation()
    {
        foreach (UIRotatingGear gear in gearsList)
        {
            gear.stopRotating();
        }
    }

}
