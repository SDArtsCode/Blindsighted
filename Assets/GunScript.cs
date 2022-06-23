using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 0.5f;

    public Camera fpsCam;

    public ParticleSystem blast;

    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    [SerializeField] AudioSource gunShot;
    // Update is called once per frame
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
       
        blast.Play();
        gunShot.Play();
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {


            targetScript target = hit.transform.GetComponent<targetScript>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactGameObject = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactGameObject, 1f);
        }




    }

    IEnumerator waitforShot()
    {
       yield return new WaitForSeconds(.75f);
    }


}
