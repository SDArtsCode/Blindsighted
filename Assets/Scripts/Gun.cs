using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Weapon weapon;

    public Weapon Weapon
    {
        get { return weapon; }
        set
        {
            Debug.Log("Weapon changed to " + value.name);

            Destroy(transform.GetChild(0));
            Instantiate(value.prefab, gameObject.transform); 
            weapon = value;
            currentWeapon = value;

            gunOrigin = GameObject.Find("GunOrigin").transform;
            muzzleFlash = GetComponentInChildren<ParticleSystem>();
        }
    }



    public static Weapon currentWeapon;
    [SerializeField] float travelTime = 3.0f;
    [SerializeField] LayerMask layerMask;
    Transform gunOrigin;

    [SerializeField] Camera fpsCam;

    private Vector3 destination;
    private float nextTimeToFire = 0f;
    private Animator anim;

    [SerializeField] GameObject bullet;
    ParticleSystem muzzleFlash;
    [SerializeField] Animator flashAnim;

    int ammoInGun;
    bool isReloading = false;

    [SerializeField] int maxAmmoInventory;
    int ammoInventory;


    private void Start()
    {
        currentWeapon = weapon;

        ammoInventory = maxAmmoInventory;

        int ammo = Mathf.Clamp(currentWeapon.magSize, 0, ammoInventory);
        ammoInGun += ammo;
        ammoInventory -= ammo;

        anim = GetComponent<Animator>();
        muzzleFlash = GetComponentInChildren<ParticleSystem>();
        gunOrigin = GameObject.Find("GunOrigin").transform;

    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time >= nextTimeToFire && ammoInGun > 0 && !isReloading)
            {
                nextTimeToFire = Time.time + 1f / currentWeapon.fireRate;
                Shoot();
            }
            else if(Time.time >= nextTimeToFire && ammoInGun <= 0 && !isReloading)
            {
                if(ammoInventory > 0)
                {
                    Reload();
                }
                else
                {
                    //play empty sfx
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(ammoInGun < currentWeapon.magSize && ammoInventory > 0)
            {
                Reload();
            }
        }
    }

    void Shoot()
    {
        ammoInGun--;
        anim.SetTrigger("Fire");

        muzzleFlash.Play();
        flashAnim.SetTrigger("Flash");
        AudioManager.instance.Play(currentWeapon.soundEffect);

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        var b = Instantiate(bullet, gunOrigin.transform.position, Quaternion.identity);
        b.GetComponent<Rigidbody>().velocity = Direction(destination, gunOrigin.transform.position) * currentWeapon.bulletSpeed;
        Destroy(b, travelTime);

    }

    IEnumerator waitforShot()
    {
       yield return new WaitForSeconds(.75f);
    }

    public void Reload()
    {
        isReloading = true;

        anim.SetTrigger("Reload");
        anim.speed = currentWeapon.reloadMultiplier;
        AudioManager.instance.Play("GunReload");   
    }

    public void ReloadFinished()
    {
        isReloading = false;

        int reloadAmount = currentWeapon.magSize - ammoInGun;
        int reloaded = Mathf.Clamp(reloadAmount, 0, ammoInventory);
        ammoInGun += reloaded;
        ammoInventory -= reloaded;
    }

    public void AddAmmo(int amount)
    {
        ammoInventory = Mathf.Clamp(ammoInventory + amount, 0, maxAmmoInventory);
    }

    Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (from-to).normalized;
    }
}
