using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLegs_Player : MonoBehaviour
{
    public LegsType type;
    public LegSpeed speed;
    public HazardEffect slowEffect;
    public HazardEffect damageEffect;


    public void ChangeLegType(LegData_Player legDataSO)
    {
        type = legDataSO.type;
        speed = legDataSO.speed;
        slowEffect = legDataSO.slowEffect;
        damageEffect = legDataSO.damageEffect;
    }
}
