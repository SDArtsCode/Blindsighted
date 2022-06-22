using UnityEngine;

public class EnemyHealth : Health
{
    public override void Death()
    {
        Debug.Log("Enemy Dead");
    }
}
