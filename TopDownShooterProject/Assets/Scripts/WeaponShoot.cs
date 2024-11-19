using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public Weapon WeaponInformation;
    public GameObject Player;
    public GameObject BulletTransform;


    public GameObject bullet;
    public bool CanFire;

    private float Timer;

    private GameObject createdBullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!CanFire)
        {
            Timer += Time.deltaTime;
            if(Timer > WeaponInformation.firerate)
            {
                CanFire = true;
                Timer = 0;
            }
        }
        if (Input.GetMouseButtonDown(0) && CanFire)
        {
            CanFire = false;
            Quaternion PositionChange = Quaternion.Euler(0, 0, 0);

            for (int i = 0; i < WeaponInformation.bulletcount; i++)
            {
                createdBullet = Instantiate(bullet, BulletTransform.transform.position, Quaternion.identity);
                PositionChange = Quaternion.Euler(0, Random.Range(0.3f, 2), 0);

            }
            createdBullet.transform.position = Player.transform.position;
        }
    }
}
