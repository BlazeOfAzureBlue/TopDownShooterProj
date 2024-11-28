using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public List<GameObject> enemies = new List<GameObject>();

    private GameObject[] enemyList; 

    // Update is called once per frame
    void Update()
    {
        enemyList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject gameObj in enemyList)
        {
            enemies.Add(gameObj);
        }
    }
}
