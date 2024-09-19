using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class LegData_Player : ScriptableObject
{
    public LegsType type;
    public LegSpeed speed;
    public HazardEffect slowEffect;
    public HazardEffect damageEffect;

}
