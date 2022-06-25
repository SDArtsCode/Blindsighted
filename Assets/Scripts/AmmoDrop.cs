using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    [SerializeField] int magAmount;
    [SerializeField] int maxMagDropAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponentInChildren<Gun>().AddAmmo(Gun.currentWeapon.magSize * (Random.Range(1, maxMagDropAmount)));
            //AudioManager.instance.Play(""); ammo pick up sfx
            Destroy(gameObject);
        }
    }
}
