using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBaseClass_Weapon : BaseClass_Weapon
{
    [Header("CONFIG (RANGED BASE CLASS)")]
    public RangedWeaponData_Weapon baseStats = null;

    [Header("DEBUG (RANGED BASE CLASS)")]
    public float damage;
    public float fireRate; //rounds per second
    public float projectileSpeed;
    public float pierce;
    public float knockback;
    public float range;

    public ElementType element;
    public GameObject projectilePrefab;

    [SerializeField] bool readyToFire;
    [SerializeField] float firingDelay;

    protected virtual void Awake()
    {
        SetBaseStats();
    }

    private void SetBaseStats()
    {
        //already set manually on baseclass_weapon
        //weaponType = baseStats.weaponType;
        damage = baseStats.damage;
        fireRate = baseStats.fireRate;
        projectileSpeed = baseStats.projectileSpeed;
        pierce = baseStats.pierce;
        knockback = baseStats.knockback;
        range = baseStats.range;
        element = baseStats.element;

        projectilePrefab = baseStats.projectilePrefab;
    }

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
            Shoot();
            readyToFire = false;
            firingDelay = 1 / fireRate;
        }
    }

    //to be overridden by subclasses
    protected virtual void Shoot()
    {
        Debug.Log("Called: Base Ranged Projectile Shoot Method");
    }

    protected void CreateProjectile(Vector2 normalizedFiringDirection)
    {
        GameObject projectileObject = Instantiate(projectilePrefab, this.transform.position, Quaternion.identity);

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

        //make projectile point towards its move direction
        projectileScript.normalizedMovementVector = normalizedFiringDirection.normalized;//for safety
        projectileScript.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(normalizedFiringDirection.y, normalizedFiringDirection.x) * Mathf.Rad2Deg);

        
    }
}
