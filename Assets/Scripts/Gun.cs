using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float bulletForce = 100;
    [SerializeField] float travelTime = 3.0f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform gunOrigin;

    [SerializeField] Camera fpsCam;

    private float nextTimeToFire = 0f;

    [SerializeField] GameObject bullet;
    [SerializeField] ParticleSystem muzzleFlash;


    void Update()
    {

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {           
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.Play();
        //gunSFX.Play();

        RaycastHit hit;

        if (Physics.Raycast(gunOrigin.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            var b = Instantiate(bullet, gunOrigin.transform.position, Quaternion.identity);
            b.GetComponent<Rigidbody>().AddForce(Direction(gunOrigin.transform.position, hit.point) * bulletForce, ForceMode.Impulse);
            Destroy(b, travelTime);
        }
    }

    IEnumerator waitforShot()
    {
       yield return new WaitForSeconds(.75f);
    }

    Vector3 Direction(Vector3 from, Vector3 to)
    {
        return (to-from).normalized;
    }

}
