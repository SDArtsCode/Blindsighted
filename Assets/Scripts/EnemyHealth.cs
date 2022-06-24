using UnityEngine;

public class EnemyHealth : Health
{
    public override void Death()
    {
        Destroy(gameObject);
        Debug.Log("Enemy Dead");
    }
}
