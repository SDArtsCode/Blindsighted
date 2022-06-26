using UnityEngine;

public class AmmoDrop : MonoBehaviour
{
    [SerializeField] int magAmount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Ammo Picked Up");
            other.GetComponentInChildren<Gun>().AddAmmo(Gun.currentWeapon.magSize * (Random.Range(1, magAmount)));
            //AudioManager.instance.Play(""); ammo pick up sfx
            Destroy(gameObject);
        }
    }
}
