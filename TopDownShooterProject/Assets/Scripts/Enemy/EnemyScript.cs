using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    [Header("Enemy Attributes")]
    [SerializeField] float health;
    [SerializeField] float maxHealth;
    [SerializeField] float moveSpeed;

    private Transform playerCharacter;
    private Rigidbody2D rigidbody;
    private PolygonCollider2D collider;
    private Vector2 moveDirection;
    private EnemyManager enemyManager;

    private float chillCountdown;
    private float chilledApplication;
    private float endChillCountdown;
    private bool beingChilled = false;
    private bool CountdownActive = false;
    private bool CanAttack = false;

    public bool BeenZapped = false;
    public bool MindBlasted = false;

    private int PoisonCounter = 0;

    private float PreviousMag;

    private bool IsFrozen;

    public GameObject HealthPickup;
    public GameObject AmmoPickup;
    PlayerHealthScript playerHealth; 

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rigidbody = GetComponent<Rigidbody2D>();
        playerCharacter = GameObject.Find("Player").transform;
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        playerHealth = GameObject.Find("GameManager").GetComponent<PlayerHealthScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if(MindBlasted == false && playerHealth.playerDead == false)
        {
            Vector3 direction = (playerCharacter.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rigidbody.rotation = angle;
            moveDirection = direction;
            rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        else
        {
            PreviousMag = 1000f;
            GameObject closestEnemy = null;
            foreach (GameObject enemy in enemyManager.enemies)
            {
                if(enemy != null)
                {
                    if ((enemy.transform.position - transform.position).magnitude < PreviousMag)
                    {
                        closestEnemy = enemy;
                    }
                }
            }
            if(closestEnemy != null)
            {
                Vector3 direction = (closestEnemy.transform.position - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rigidbody.rotation = angle;
                moveDirection = direction;
                rigidbody.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
            }
        }
        if(chilledApplication >= 1)
        {
            endChillCountdown += Time.deltaTime;
            if (endChillCountdown >= 1)
            {
                chilledApplication = 0;
                moveSpeed = 5;
                CountdownActive = false;
            }
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && MindBlasted == true)
        {
            EnemyScript enemyCode = gameObject.gameObject.GetComponent<EnemyScript>();
            enemyCode.TakeDamage(1);
        }
        else if(collision.gameObject.CompareTag("Player"))
        { 
            playerHealth.OnHit();
        }
    }
    public void IncreasePoison()
    {
        PoisonCounter += 1;
        if(PoisonCounter >= 5)
        {
            StartCoroutine(TakePoisonDamage());
            PoisonCounter = 0;
        }
    }

    IEnumerator TakePoisonDamage()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSecondsRealtime(1);
            TakeDamage(5);
        }
    }
    public void BeginFreeze()
    {
        StartCoroutine(GetFrozen());
    }

    public void GetGrappled()
    {
        StartCoroutine(Stun(1));
    }

    public void StartMindControl()
    {
        MindBlasted = true;
        StartCoroutine(MindControlTimer());
    }

    private IEnumerator MindControlTimer()
    {
        yield return new WaitForSecondsRealtime(10);
        MindBlasted = false;
    }
    public IEnumerator GetFrozen()
    {
        if(IsFrozen == false)
        {
            IsFrozen = true;
            moveSpeed = 0;
            CanAttack = false;
            yield return new WaitForSecondsRealtime(3);
            moveSpeed = 5;
            CanAttack = true;
            IsFrozen = false;
        }
    }

    public void GetPunched(int StunTimer, Vector3 Direction)
    {
        StartCoroutine(Stun(StunTimer));
        Knockback(Direction);
    }
    private IEnumerator Stun(int StunTimer)
    {
        moveSpeed = 0;
        CanAttack = false;
        yield return new WaitForSeconds(StunTimer);
        moveSpeed = 5;
        CanAttack = true;
    }

    private void  Knockback(Vector3 direction)
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(direction * 100, ForceMode2D.Impulse);
    }
    public void BeginChilling()
    {
        chillCountdown += Time.deltaTime;
        beingChilled = true;
        if(chillCountdown > 0.5 && beingChilled == true)
        {
            chillCountdown = 0;
            Chill();
        }
    }
    private void Chill()
    {
        if(chilledApplication < 5)
        {
            chilledApplication += 1;
            moveSpeed = 5 - (chilledApplication / 2);
            endChillCountdown = 0;
        }
        else
        {
            endChillCountdown = 0;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if(IsFrozen == true && damageAmount > 0.1)
        {
            Destroy(gameObject);
        }
        health -= damageAmount;
        if(health <= 0)
        {
            int RandomChance = UnityEngine.Random.Range(0, 11);
            if(RandomChance == 5)
            {
                int PickupDecision = UnityEngine.Random.Range(0, 2);
                if(PickupDecision == 0)
                {
                    Instantiate(HealthPickup, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(AmmoPickup, transform.position, Quaternion.identity);
                }
            }
            Destroy(gameObject);
        }
    }
}
