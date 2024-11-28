using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMenuButton : MonoBehaviour
{

    AmmoManagement AmmoManager;
    GameObject gun;

    public Weapon containedWeapon;

    // Start is called before the first frame update
    void Start()
    {
        AmmoManager = GameObject.Find("AmmoManager").GetComponent<AmmoManagement>();
        gun = GameObject.Find("Gun");

        
    }

    public void ChangeWeapon()
    {
        int newWeaponIndex = AmmoManager.WeaponList.IndexOf(containedWeapon);
        AmmoManager.UpdateWeapon(containedWeapon);
        gun.GetComponent<WeaponShoot>().WeaponInformation = AmmoManager.WeaponList[newWeaponIndex];
        gun.GetComponent<WeaponShoot>().UpdateBullet();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
