using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public float damage;
    [SerializeField] public float firerate;
    [SerializeField] public float bulletcount;
    [SerializeField] public MonoScript SpecialFunction;


}
