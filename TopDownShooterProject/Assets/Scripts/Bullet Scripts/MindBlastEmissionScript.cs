using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MindBlastEmissionScript : MonoBehaviour
{

    float time;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
        {
            EnemyScript enemyCode = collision.GetComponent<EnemyScript>();
            enemyCode.StartMindControl();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        if(time >= 0.1)
        {
            Destroy(gameObject);
        }
    }
}
