using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoManagement : MonoBehaviour
{
    List<int> GunTotalAmmo = new List<int>();
    List<int> GunCurrentMagazine = new List<int>();
    
    public List<Weapon> WeaponList = new List<Weapon>();
    public Weapon currentWeapon;
    public int CurrentGunMaxAmmo;
    public int CurrentGunCurrentReserveAmmo;
    public int CurrentGunCurrentMag;
    public int CurrentGunMaxMag;
    public float CurrentGunReloadTime;

    public TMP_Text WeaponName;
    public TMP_Text AmmoCount;

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
        WeaponName.text = WeaponList[0].WeaponName;
        AmmoCount.text = CurrentGunCurrentMag.ToString() + " / " + CurrentGunCurrentReserveAmmo.ToString();
        currentWeapon = GameObject.Find("Gun").GetComponent<WeaponShoot>().WeaponInformation;

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
        if(WeaponName.text != "Minigun")
        {
            AmmoCount.text = CurrentGunCurrentMag.ToString() + " / " + CurrentGunCurrentReserveAmmo.ToString();
        }
        else
        {
            AmmoCount.text = CurrentGunCurrentMag.ToString();
        }
        
    }

    public void UpdateWeapon(Weapon newWeapon)
    {
        currentWeapon = GameObject.Find("Gun").GetComponent<WeaponShoot>().WeaponInformation;
        int Index = WeaponList.IndexOf(currentWeapon);
        GunTotalAmmo[Index] = CurrentGunCurrentReserveAmmo;
        GunCurrentMagazine[Index] = CurrentGunCurrentMag;

        Index = WeaponList.IndexOf(newWeapon);
        CurrentGunCurrentReserveAmmo = GunTotalAmmo[Index];
        CurrentGunCurrentMag = GunCurrentMagazine[Index];
        CurrentGunReloadTime = newWeapon.ReloadTime;
        CurrentGunMaxMag = newWeapon.MagazineSize;
        WeaponName.text = newWeapon.WeaponName;
        if(newWeapon.WeaponName != "Minigun" && newWeapon.WeaponName != "Fist" && currentWeapon.WeaponName != "Chainsaw")
        {
            AmmoCount.text = CurrentGunCurrentMag.ToString() + " / " + CurrentGunCurrentReserveAmmo.ToString();
        }
        else if(newWeapon.WeaponName == "Minigun")
        {
            AmmoCount.text = CurrentGunCurrentMag.ToString();
        }
        else
        {
            AmmoCount.text = "";
        }
    }

    public void UpdateAmmo()
    {
        currentWeapon = GameObject.Find("Gun").GetComponent<WeaponShoot>().WeaponInformation;
        int Index = WeaponList.IndexOf(currentWeapon);
        int IncreaseAmount = Convert.ToInt32(Math.Round(currentWeapon.MaxAmmo * 0.2));
        if (currentWeapon.WeaponName != "Minigun" && currentWeapon.WeaponName != "Fist" && currentWeapon.WeaponName != "Chainsaw")
        {
            CurrentGunCurrentReserveAmmo += IncreaseAmount;
            if (CurrentGunCurrentReserveAmmo > currentWeapon.MaxAmmo)
            {
                CurrentGunCurrentReserveAmmo = currentWeapon.MaxAmmo;
            }
        }
        else if (currentWeapon.WeaponName == "Minigun")
        {
            CurrentGunCurrentMag += IncreaseAmount;
            if (CurrentGunCurrentMag > currentWeapon.MaxAmmo)
            {
                CurrentGunCurrentMag = currentWeapon.MaxAmmo;
            }
        }
        GunTotalAmmo[Index] = CurrentGunCurrentReserveAmmo;
        if (currentWeapon.WeaponName != "Minigun" && currentWeapon.WeaponName != "Fist" && currentWeapon.WeaponName != "Chainsaw")
        {
            AmmoCount.text = CurrentGunCurrentMag.ToString() + " / " + CurrentGunCurrentReserveAmmo.ToString();
        }
        else if (currentWeapon.WeaponName == "Minigun")
        {
            AmmoCount.text = CurrentGunCurrentMag.ToString();
        }
        else
        {
            AmmoCount.text = "";
        }
    }

    IEnumerator Reload()
    {
        print(CurrentGunCurrentReserveAmmo);
        AmmoCount.text = "Reloading...";
        yield return new WaitForSeconds(CurrentGunReloadTime);
        if((CurrentGunCurrentReserveAmmo - (CurrentGunMaxMag - CurrentGunCurrentMag)) > 0)
        {
            CurrentGunCurrentReserveAmmo = CurrentGunCurrentReserveAmmo - (CurrentGunMaxMag - CurrentGunCurrentMag);
            CurrentGunCurrentMag = CurrentGunMaxMag;
        }
        else if(CurrentGunCurrentReserveAmmo > 0)
        {
            if((CurrentGunCurrentMag + CurrentGunCurrentReserveAmmo) > CurrentGunMaxMag)
            {
                int AmmoChange = CurrentGunMaxMag - CurrentGunCurrentMag;
                print(AmmoChange);
                CurrentGunCurrentReserveAmmo -= AmmoChange;
                CurrentGunCurrentMag += AmmoChange;
            }
            else
            {
                CurrentGunCurrentMag += CurrentGunCurrentReserveAmmo;
                CurrentGunCurrentReserveAmmo = 0;
            }
        }
        AmmoCount.text = CurrentGunCurrentMag.ToString() + " / " + CurrentGunCurrentReserveAmmo.ToString();
    }
}
