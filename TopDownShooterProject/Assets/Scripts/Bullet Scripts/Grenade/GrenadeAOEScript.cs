using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAOEScript : MonoBehaviour
{
    GameObject gun;
    private void Start()
    {
        gun = GameObject.Find("Gun");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            EnemyScript enemyCode = collision.transform.GetComponent<EnemyScript>();
            enemyCode.TakeDamage(gun.GetComponent<WeaponShoot>().WeaponInformation.damage);
        }
    }
}
