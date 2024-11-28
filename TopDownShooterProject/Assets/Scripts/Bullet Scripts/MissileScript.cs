using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider;
    private float weaponDamage;

    public GameObject gun;
    public GameObject explosion;
    private GameObject CreatedExplosion;

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

    
    private void OnTriggerEnter2D(Collider2D collision)
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
            CreatedExplosion = Instantiate(explosion, this.transform.position, Quaternion.identity);
            CreatedExplosion.transform.position = this.transform.position;
            Destroy(transform.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
