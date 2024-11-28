using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunSpecial : MonoBehaviour
{

    public WeaponShoot weapon;
    public GameObject Player;

    public GameObject BulletTransform;
    public GameObject bullet;

    private GameObject createdBullet1;
    private GameObject createdBullet2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && weapon.CanFire)
        {
            Quaternion PositionChange = Quaternion.Euler(0, 45, 0);
            createdBullet1 = Instantiate(bullet, BulletTransform.transform.position, Quaternion.identity * PositionChange);
            createdBullet1.transform.position = Player.transform.position;
        }
    }

    public void SpecialFunction()
    {

    }
}
