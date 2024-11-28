using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponCreation", menuName = "ScriptableObjects/NewWeapon", order = 1)]
public class Weapon : ScriptableObject
{
    [Header("Required components")]
    [SerializeField] public AudioSource audio;
    [SerializeField] public SpriteRenderer weaponSprite;
    [SerializeField] public GameObject weapon;

    [Header("Weapon Attributes")]
    [SerializeField] public string WeaponName;
    [SerializeField] public float damage;
    [SerializeField] public float firerate;
    [SerializeField] public float bulletcount;
    [SerializeField] public GameObject bullet;
    [SerializeField] public bool HeldDownWeapon;

    [Header("Ammo Attributes")]
    [SerializeField] public int MaxAmmo;
    [SerializeField] public int CurrentReserves;
    [SerializeField] public int MagazineSize;
    [SerializeField] public int CurrentAmmo;
    [SerializeField] public float ReloadTime;
    [SerializeField] public bool Reloadable;


}
