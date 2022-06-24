using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
