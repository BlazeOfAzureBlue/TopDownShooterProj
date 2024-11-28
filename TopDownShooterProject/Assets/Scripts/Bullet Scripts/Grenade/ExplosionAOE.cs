using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionAOE : MonoBehaviour
{

    private CircleCollider2D circleCollider;
    private GameObject gun;
    private float weaponDamage;

    private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeOut());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            print("ok");
            gun = GameObject.Find("Gun");
            weaponDamage = gun.GetComponent<WeaponShoot>().WeaponInformation.damage / 2;
            EnemyScript enemyCode = collision.transform.GetComponent<EnemyScript>();
            enemyCode.TakeDamage(weaponDamage);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator FadeOut()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, i);
            yield return null;
        }
        if (sprite.color.a >= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
