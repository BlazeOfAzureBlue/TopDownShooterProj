using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManagement : MonoBehaviour
{
    List<int> GunTotalAmmo = new List<int>();
    List<int> GunCurrentMagazine = new List<int>();
    
    public List<Weapon> WeaponList = new List<Weapon>();
    public int CurrentGunMaxAmmo;
    public int CurrentGunCurrentReserveAmmo;
    public int CurrentGunCurrentMag;
    public int CurrentGunMaxMag;
    public float CurrentGunReloadTime;

    // 1. Rifle, 2. Shotgun, 3. Flamethrower, 4. Missile Launcher, 5. Minigun, 6. Chainsaw, 7. Razor Blades, 8. Frost Gun, 9. Orb of Fire, 10. Glove, 11. Chain Lightning, 12. Grenade, 13. Grappling Hook, 14. Mind Blast, 15. Crossbow
    // Start is called before the first frame update
    void Start()
    {
        foreach(Weapon weapon in WeaponList)
        {
            GunTotalAmmo.Add(weapon.MaxAmmo);
            GunCurrentMagazine.Add(weapon.CurrentAmmo);
        }
        CurrentGunCurrentReserveAmmo = GunTotalAmmo[0];
        CurrentGunCurrentMag = GunCurrentMagazine[0];
        CurrentGunReloadTime = WeaponList[0].ReloadTime;
        CurrentGunMaxMag = WeaponList[0].MagazineSize;
    }

    // ideas before I sleep:
    // whenever the player changes weapons, set the current int to that
    // don't need to bother with the other two lists
    // then handle all reload functions in here
    // check if the player presses R then reload
    // but call upon AmmoManager in WeaponShoot
    // and check if it has enough ammo

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && CurrentGunCurrentMag != CurrentGunMaxMag && CurrentGunCurrentReserveAmmo != 0)
        {
            StartCoroutine(Reload());
        }
    }


    public void FireShot()
    {
        CurrentGunCurrentMag -= 1;
        print(CurrentGunCurrentMag);
    }

    public void UpdateWeapon(Weapon newWeapon)
    {
        int Index = WeaponList.IndexOf(newWeapon);

        CurrentGunCurrentReserveAmmo = GunTotalAmmo[Index];
        CurrentGunCurrentMag = GunCurrentMagazine[Index];
        CurrentGunReloadTime = newWeapon.ReloadTime;
        CurrentGunMaxMag = newWeapon.MagazineSize;
    }
    IEnumerator Reload()
    {
        print(CurrentGunCurrentReserveAmmo);
        yield return new WaitForSeconds(CurrentGunReloadTime);
        CurrentGunCurrentReserveAmmo = CurrentGunCurrentReserveAmmo - (CurrentGunMaxMag -  CurrentGunCurrentMag);
        CurrentGunCurrentMag = CurrentGunMaxMag;
    }
}
