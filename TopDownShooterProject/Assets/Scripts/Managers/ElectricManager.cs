using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricManager : MonoBehaviour
{

    public void ActivateDestruction(GameObject bullet)
    {
        StartCoroutine(DestroyLightningAOE(bullet));
    }
    private IEnumerator DestroyLightningAOE(GameObject bullet)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        Destroy(bullet);
    }
}
