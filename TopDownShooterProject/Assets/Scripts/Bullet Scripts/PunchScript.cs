using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchScript : MonoBehaviour
{

    private BoxCollider2D boxcollider;
    private GameObject gun;
    private float weaponDamage;



    // Start is called before the first frame update
    void Start()
    {
       gun = GameObject.Find("Gun");
        weaponDamage = gun.GetComponent<WeaponShoot>().WeaponInformation.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            EnemyScript enemyCode = collision.transform.GetComponent<EnemyScript>();
            enemyCode.TakeDamage(weaponDamage);
            enemyCode.GetPunched(2, direction);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
