using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireballMovement : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider;
    private float weaponDamage;

    private float BeginningXTransform;
    private float BeginningYTransform;
    private float pingPong;


    private float _frequency = 1.0f;
    private float _ampltude = 5.0f;
    private float _cycleSpeed = 1.0f;

    private int i = 50;
    private bool currentDirection = false;
    private Vector3 pos;
    private Vector3 axis;

    public GameObject gun;

    private List<GameObject> enemyList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.Find("Gun");
        weaponDamage = gun.GetComponent<WeaponShoot>().WeaponInformation.damage;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos;
        rigidbody.AddForce(gun.transform.right * 0.0001f, ForceMode2D.Impulse);

        pos = transform.position;
        axis = transform.right;
        StartCoroutine(ActivateDamage());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player") && !collision.transform.CompareTag("Bullet"))
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                if(enemyList.Count < 3)
                {
                    enemyList.Add(collision.transform.gameObject);
                }
            }
            if(!collision.transform.CompareTag("Enemy"))
            {
                Destroy(transform.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if(enemyList.Contains(collision.transform.gameObject))
            {
                enemyList.Remove(collision.transform.gameObject);
            }
        }
    }

    IEnumerator ActivateDamage()
    {
        while (true)
        {
            print("ok");
            yield return new WaitForSeconds(1);
            for (int i = 0; i < enemyList.Count; i++)
            {
                print(enemyList[i].gameObject.name);
                EnemyScript enemyCode = enemyList[i].transform.GetComponent<EnemyScript>();
                enemyCode.TakeDamage(weaponDamage);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
