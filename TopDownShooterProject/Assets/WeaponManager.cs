using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    AmmoManagement AmmoManager;
    // Start is called before the first frame update
    void Start()
    {
        AmmoManager = GameObject.Find("AmmoManager").GetComponent<AmmoManagement>();
        foreach(Weapon weapon in AmmoManager.WeaponList)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
