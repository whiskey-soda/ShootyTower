using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBaseClass_Weapon : MonoBehaviour
{
    [Header("CONFIG")]
    [SerializeField] float damage;
    [SerializeField] float fireRate; //rounds per second
    [SerializeField] float projectileSpeed;
    [SerializeField] float pierce;
    [SerializeField] float knockback;
    [SerializeField] float range;
    [SerializeField] ElementType element;
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] HeightLevel heightLevel;

    [Header("DEBUG")]
    [SerializeField] bool readyToFire;
    [SerializeField] float firingDelay;

    private void Update()
    {
        if (firingDelay > 0)
        {
            firingDelay -= Time.deltaTime;
        }
        else
        {
            readyToFire = true;
        }

        if (readyToFire)
        {
            CreateProjectile(new Vector2(1,1));
            readyToFire = false;
            firingDelay = 1 / fireRate;
        }
    }

    void Shoot()
    {
        Debug.Log("Called: Base Ranged Projectile Shoot Method");
    }

    void CreateProjectile(Vector2 normalizedFiringDirection)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        BaseClass_Projectile projectileScript = projectileObject.GetComponent<BaseClass_Projectile>();

        ConfigureProjectile(projectileScript, normalizedFiringDirection.normalized);
    }

    /// <summary>
    /// gives projectile all relevant stat values from the weapon, and gives it appropriate rotation and movement info.
    /// </summary>
    /// <param name="projectileScript"></param>
    /// <param name="normalizedFiringDirection"></param>
    private void ConfigureProjectile(BaseClass_Projectile projectileScript, Vector2 normalizedFiringDirection)
    {
        //give bullet all values
        projectileScript.damage = damage;
        projectileScript.speed = projectileSpeed;
        projectileScript.pierce = pierce;
        projectileScript.knockback = knockback;
        projectileScript.range = range;
        projectileScript.element = element;
        projectileScript.myHeightLevel = heightLevel;

        projectileScript.normalizedMovementVector = normalizedFiringDirection.normalized;

        projectileScript.transform.LookAt(new Vector3(transform.position.x + normalizedFiringDirection.x, transform.position.y + normalizedFiringDirection.y, transform.position.z));
    }
}
