using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChainLightningAOE : MonoBehaviour
{

    private CircleCollider2D circlecollider;
    private List<GameObject> enemies = new List<GameObject>();
    private GameObject ClosestEnemy = null;
    private int LightningCounter;
    private GameObject createdBullet;
    private bool triggered = false;

    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        circlecollider = GetComponent<CircleCollider2D>();
        gun = GameObject.Find("Gun");
        LightningCounter = gun.GetComponent<WeaponShoot>().ChainLightningCounter;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            EnemyScript enemyCode = collision.GetComponent<EnemyScript>();
            if (enemyCode.BeenZapped == false)
            {
                enemyCode.BeenZapped = true;
                enemies.Add(collision.transform.gameObject);
                if (enemies.Count != 0)
                {
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        GameObject enemy = enemies[i];
                        if (ClosestEnemy != null)
                        {
                            if (((transform.position - enemy.transform.position).sqrMagnitude < (transform.position - ClosestEnemy.transform.position).sqrMagnitude))
                            {
                                ClosestEnemy = enemy;
                            }
                        }
                        else
                        {
                            ClosestEnemy = enemy;
                        }
                    }
                    if (!(LightningCounter >= 3))
                    {
                        LightningCounter += 1;
                        GameObject bullet = gun.GetComponent<WeaponShoot>().WeaponInformation.bullet;
                        createdBullet = Instantiate(bullet, ClosestEnemy.gameObject.transform.position, Quaternion.identity);
                        GameObject electricmanage = GameObject.Find("GameManager");
                        ElectricManager removeelectric = electricmanage.AddComponent<ElectricManager>();
                        enemyCode.TakeDamage(gun.GetComponent<WeaponShoot>().WeaponInformation.damage * (gun.GetComponent<WeaponShoot>().WeaponInformation.damage * (0.15f * LightningCounter)));
                        removeelectric.ActivateDestruction(createdBullet);
                    }
                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
