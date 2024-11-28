using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
   public PlayerHealthScript healthScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            if(healthScript.PlayerHealth != 3)
            {
                healthScript.IncreaseHealth();
                Destroy(gameObject);
            }
        }
    }
}
