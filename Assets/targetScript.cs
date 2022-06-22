using UnityEngine;

public class targetScript : MonoBehaviour
{

    public float healthCount;

    public void TakeDamage(float amount)
    {
        healthCount += amount;

        if(healthCount >= 100f)
        {
            Die();
        }


        void Die()
        {
            Destroy(gameObject);
        }
    }

}
