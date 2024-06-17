using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archers_Weapon : RangedBaseClass_Weapon
{

    private void Reset()
    {
        //set all defaults
        weaponType = WeaponType.Archers;
        damage = 5;
        fireRate = .6f;
        projectileSpeed = 15;
        pierce = 2;
        knockback = 1;
        range = 5;
        element = ElementType.None;
    }

    protected override void Shoot()
    {
        //shoot in 6 directions
        CreateProjectile(new Vector2(1, 0));
        CreateProjectile(new Vector2(.5f, -.866f));
        CreateProjectile(new Vector2(-.5f, -.866f));
        CreateProjectile(new Vector2(-1, 0));
        CreateProjectile(new Vector2(-.5f, .866f));
        CreateProjectile(new Vector2(.5f, .866f));
    }

}
