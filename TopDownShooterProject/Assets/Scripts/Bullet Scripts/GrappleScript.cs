using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider;
    private float weaponDamage;
    private GameObject player;

    public GameObject gun;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            StartCoroutine(BeginPullEnemy(collision.gameObject));
        }
        else
        {
            rigidbody.simulated = false;
            StartCoroutine(BeginPull(player));
        }
    }

    private IEnumerator BeginPullEnemy(GameObject enemy)
    {
        while ((enemy.transform.position - player.transform.position).magnitude > 0.1)
        {
            yield return new WaitForEndOfFrame();
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, 0.1f);
        }
        EnemyScript enemyCode = enemy.GetComponent<EnemyScript>();
        enemyCode.GetGrappled();
        Destroy(transform.gameObject);
    }

    private IEnumerator BeginPull(GameObject player)
    {
        while ((transform.position - player.transform.position).magnitude > 0.1)
        {
            yield return new WaitForEndOfFrame();
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, 0.1f);
        }
        Destroy(transform.gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gun = GameObject.Find("Gun");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos;
        rigidbody.AddForce(gun.transform.right * 0.001f, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }

    
}

