using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class Cannon_Weapon : RangedBaseClass_Weapon
{
    private void Reset()
    {
        //set all defaults
        weaponType = WeaponType.Cannon;
        damage = 10;
        fireRate = .4f;
        projectileSpeed = 10;
        pierce = 6;
        knockback = 4;
        range = 7.5f;
        element = ElementType.None;
    }
    protected override void Shoot()
    {
        //shoot in 4 diagonals
        CreateProjectile(new Vector2(1, 1));
        CreateProjectile(new Vector2(1, -1));
        CreateProjectile(new Vector2(-1, -1));
        CreateProjectile(new Vector2(-1, 1));
    }

}
