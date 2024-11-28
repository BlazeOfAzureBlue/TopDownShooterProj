using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeScript : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider;
    private float weaponDamage;

    private int hitCounter = 0;

    public GameObject gun;


    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("Gun");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos;
        rigidbody.AddForce(gun.transform.right * 0.001f, ForceMode2D.Impulse);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.transform.CompareTag("Player") && !collision.transform.CompareTag("Bullet"))
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                print("ok");
                weaponDamage = gun.GetComponent<WeaponShoot>().WeaponInformation.damage;
                EnemyScript enemyCode = collision.transform.GetComponent<EnemyScript>();
                enemyCode.TakeDamage(weaponDamage);
            }
            Vector3 velocity = Vector3.Reflect(transform.right, collision.contacts[0].normal);
            float rot = 90 - Mathf.Atan2(velocity.z, velocity.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, rot);
            hitCounter += 1;

            if (hitCounter >= 5)
            {
                Destroy(transform.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
