using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupScript : MonoBehaviour
{
    public AmmoManagement ammoScript;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            ammoScript.UpdateAmmo();
            Destroy(gameObject);
        }
    }
}
