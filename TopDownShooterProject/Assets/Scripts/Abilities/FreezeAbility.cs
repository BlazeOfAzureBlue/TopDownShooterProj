using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeAbility : MonoBehaviour
{
    [Header("Ability Prefab")]
    [SerializeField] public GameObject FreezeAOEPrefab;

    [Header("Player and Enemy requirements")]
    [SerializeField] public GameObject Player;

    private bool FreezeAbilityAvailable = true;
    private GameObject FreezeAOE;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && FreezeAbilityAvailable == true)
        {
            FreezeAbilityAvailable = false;
            FreezeAOE = Instantiate(FreezeAOEPrefab, Player.transform.position, Quaternion.identity);
            StartCoroutine(ActivateAOE());
        }
    }

    private IEnumerator ActivateAOE()
    {
        yield return new WaitForSeconds(1);
        Destroy(FreezeAOE);
        StartCoroutine(StartCooldownTimer());
    }

    private IEnumerator StartCooldownTimer()
    {
        yield return new WaitForSeconds(5);
        FreezeAbilityAvailable = true;
    }
}
