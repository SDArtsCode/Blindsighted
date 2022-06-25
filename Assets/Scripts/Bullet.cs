using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float damage;
    MeshRenderer mr;
    Rigidbody rb;
    ParticleSystem ps;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        mr = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().TakeDamage(damage);
            Explode();
        }
    }

    void Explode()
    {
        mr.enabled = false;
        damage = 0;
        rb.velocity = Vector3.zero;
        ps.Play();
    }
}
