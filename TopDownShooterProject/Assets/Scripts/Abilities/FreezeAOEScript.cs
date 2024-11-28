using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAOEScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            EnemyScript enemyCode = collision.transform.GetComponent<EnemyScript>();
            enemyCode.BeginFreeze();
        }
    }
}
