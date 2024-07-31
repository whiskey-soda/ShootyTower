using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class Cannon_Weapon : RangedBaseClass_Weapon
{
    protected override void Shoot()
    {
        //shoot in 4 diagonals
        CreateProjectile(new Vector2(1, 1));
        CreateProjectile(new Vector2(1, -1));
        CreateProjectile(new Vector2(-1, -1));
        CreateProjectile(new Vector2(-1, 1));
    }

}
