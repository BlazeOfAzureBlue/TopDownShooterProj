using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{

    public GameObject enemySpawn;
    public PlayerHealthScript playerHealth;

    private float Timer;
    
    private bool Active = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Active && playerHealth.playerDead == false)
        {
            Timer += Time.deltaTime;
            if (Timer > 4)
            {
                Active = false;
                GameObject enemy = Instantiate(enemySpawn, transform.position, Quaternion.identity);
                enemy.layer = 3;
                Timer = 0;
            }
        }

    }
}
