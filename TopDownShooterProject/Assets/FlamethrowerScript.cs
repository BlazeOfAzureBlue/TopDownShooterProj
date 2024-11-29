using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerScript : MonoBehaviour
{
    bool CanDamage;
    float Timer;

    public WeaponShoot weapon;
    private void Update()
    {
        if (!CanDamage)
        {
            Timer += Time.deltaTime;
            if (Timer > 0.2)
            {
                CanDamage = true;
                Timer = 0;
            }
        }
    }

    private void Start()
    {
        weapon = GameObject.Find("Gun").GetComponent<WeaponShoot>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            if(CanDamage)
            {
                
                EnemyScript enemyCode = collision.transform.GetComponent<EnemyScript>();
                enemyCode.TakeDamage(weapon.WeaponInformation.damage);

            }
        }
    }
}
