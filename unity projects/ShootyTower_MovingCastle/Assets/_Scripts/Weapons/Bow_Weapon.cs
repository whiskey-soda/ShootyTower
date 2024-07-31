using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow_Weapon : RangedBaseClass_Weapon
{

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
