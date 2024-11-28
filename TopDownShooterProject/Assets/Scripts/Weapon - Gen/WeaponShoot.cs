using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class WeaponShoot : MonoBehaviour
{
    [Header("Player Information")]
    public Weapon WeaponInformation;
    public GameObject Player;
    public GameObject gun;
    public AmmoManagement AmmoManager;
    private Vector3 mousePos;
    private Camera mainCamera;

    // Bullet Information
    private GameObject BulletTransform;
    private GameObject bullet;
    public bool CanFire;
    private float Timer;

    // Chain Lightning Information
    public int ChainLightningCounter;

    // Weapons Active
    private bool FlamethrowerActive = true;
    private bool FistActive = false;

    // Time Held variables
    private float MinigunTimeHeld;
    private float PunchTimeHeld;
    private float GrenadeTimeHeld;
    private float CrossbowTimeHeld;

    // Weapon Being Held variables
    private bool GrenadeBeingHeld;
    private bool CrossbowBeingHeld;
    private bool FrostRayBeingHeld;

    private GameObject createdBullet;

    [SerializeField] private float defDistanceRay = 100;
    [SerializeField] public GameObject laserFirePoint;
    [SerializeField] public LineRenderer m_lineRenderer;
    Transform m_transform;


    // Start is called before the first frame update
    void Start()
    {
        bullet = WeaponInformation.bullet;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        BulletTransform = GameObject.Find("BulletTransform");

        m_transform = GetComponent<Transform>();
        laserFirePoint = GameObject.Find("BulletTransform");
        m_lineRenderer = GetComponent<LineRenderer>();
    }


    public void UpdateBullet()
    {
        bullet = WeaponInformation.bullet;
    }
    void ShootLaser()
    {
        if (Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
            Draw2DRay(laserFirePoint.transform.position, _hit.point);
            if(_hit.collider.gameObject.tag == "Enemy")
            {
                EnemyScript enemyCode = _hit.collider.gameObject.GetComponent<EnemyScript>();
                enemyCode.BeginChilling();
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.transform.position, laserFirePoint.transform.right);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(FrostRayBeingHeld == false)
        {
            m_lineRenderer.enabled = false;
        }
            if(!CanFire)
            {
                Timer += Time.deltaTime;
                if(Timer > WeaponInformation.firerate)
                {
                    CanFire = true;
                    Timer = 0;
                }
            }

        if(Input.GetMouseButtonUp(0))
        {
            MinigunTimeHeld = 0;
            if (WeaponInformation.WeaponName == "Flamethrower" && FlamethrowerActive == false)
            {
                Destroy(createdBullet);
                FlamethrowerActive = true;
            }
            if(WeaponInformation.WeaponName == "Fist" && FistActive == true)
            {
                Destroy(createdBullet);
                FistActive = false;
                PunchTimeHeld = 0;
            }
            if(WeaponInformation.WeaponName == "Grenade" && GrenadeBeingHeld == true)
            { 
                createdBullet = Instantiate(bullet, BulletTransform.transform.position, Quaternion.identity);
                createdBullet.transform.position = BulletTransform.transform.position;
                GrenadeScript grenadeCode = createdBullet.GetComponent<GrenadeScript>();
                grenadeCode.StartThrow(GrenadeTimeHeld);
                GrenadeBeingHeld = false;
                GrenadeTimeHeld = 0;
            }
            if (WeaponInformation.WeaponName == "Crossbow" && CrossbowBeingHeld == true)
            {
                createdBullet = Instantiate(bullet, BulletTransform.transform.position, Quaternion.identity);
                createdBullet.transform.position = BulletTransform.transform.position;
                CrossbowScript crossbowCode = createdBullet.GetComponent<CrossbowScript>();
                crossbowCode.StartThrow(CrossbowTimeHeld);
                CrossbowBeingHeld = false;
                CrossbowTimeHeld = 0;
            }
            if(WeaponInformation.WeaponName == "Frost Ray")
            {

                m_lineRenderer.enabled = false;
            }
        }
        if(Input.GetMouseButton(0) && CanFire)
        {
            if(WeaponInformation.WeaponName == "Flamethrower" && FlamethrowerActive == true && AmmoManager.CurrentGunCurrentMag > 0)
            {
                mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                 Vector3 direction = mousePos;
                createdBullet = Instantiate(bullet, gun.transform.position + transform.right * 2, gun.transform.rotation);
                createdBullet.transform.parent = BulletTransform.transform;
                FlamethrowerActive = false;
            }
            if(WeaponInformation.WeaponName == "Flamethrower" && FlamethrowerActive == false)
            {
                print("Ok");
                AmmoManager.FireShot();
                if(AmmoManager.CurrentGunCurrentMag < 0)
                {
                    FlamethrowerActive = true;
                    Destroy(createdBullet);
                }
            }
            if (WeaponInformation.WeaponName == "Minigun")
            {
                MinigunTimeHeld += Time.deltaTime;
                if(MinigunTimeHeld > 2 && CanFire && AmmoManager.CurrentGunCurrentMag > 0)
                {
                    AmmoManager.FireShot();
                    CanFire = false;
                    createdBullet = Instantiate(bullet, BulletTransform.transform.position, Quaternion.identity);
                    LineRenderer lineRenderer = gun.GetComponent<LineRenderer>();
                    MeshCollider meshCollider = gun.AddComponent<MeshCollider>();

                    Mesh mesh = new Mesh();
                    lineRenderer.BakeMesh(mesh, true);
                    meshCollider.sharedMesh = mesh;

                    
                }
            }
            if (WeaponInformation.WeaponName == "Fist")
            {
                print(PunchTimeHeld);
                PunchTimeHeld += Time.deltaTime;
                if(PunchTimeHeld > 1 && CanFire && FistActive == false)
                {
                    FistActive = true;
                    CanFire = false;
                    mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    Vector3 direction = mousePos;
                    createdBullet = Instantiate(bullet, BulletTransform.transform.position + new Vector3(direction.x, direction.y).normalized * 1.6f, BulletTransform.transform.rotation);
                    createdBullet.transform.parent = BulletTransform.transform;
                }
                if (PunchTimeHeld > 1.1)
                {
                    FistActive = false;
                    CanFire = true;
                    Destroy(createdBullet);
                    PunchTimeHeld = 0;
                }
            }

            if(WeaponInformation.WeaponName == "Frost Gun" && AmmoManager.CurrentGunCurrentMag > 0)
            {
                AmmoManager.FireShot();
                m_lineRenderer.enabled = true;
                ShootLaser();
            }
            else if(WeaponInformation.WeaponName == "Frost Gun" && AmmoManager.CurrentGunCurrentMag < 0)
            {
                m_lineRenderer.enabled = false;
            }
            if (WeaponInformation.WeaponName == "Grenade")
            {
                GrenadeBeingHeld = true;
                GrenadeTimeHeld += Time.deltaTime;
                AmmoManager.FireShot();
                if (GrenadeTimeHeld >= 3)
                {
                    createdBullet = Instantiate(bullet, BulletTransform.transform.position, Quaternion.identity);
                    createdBullet.transform.position = BulletTransform.transform.position;
                    GrenadeScript grenadeCode = createdBullet.GetComponent<GrenadeScript>();
                    grenadeCode.StartThrow(GrenadeTimeHeld);
                    GrenadeTimeHeld = 0;
                    GrenadeBeingHeld = false;
                    CanFire = false;
                }
            }
            if (WeaponInformation.WeaponName == "Crossbow")
            {
                CrossbowBeingHeld = true;
                CrossbowTimeHeld += Time.deltaTime;
                AmmoManager.FireShot();
                if (CrossbowTimeHeld >= 5)
                {
                    createdBullet = Instantiate(bullet, BulletTransform.transform.position, Quaternion.identity);
                    createdBullet.transform.position = BulletTransform.transform.position;
                    CrossbowScript crossbowCode = createdBullet.GetComponent<CrossbowScript>();
                    crossbowCode.StartThrow(CrossbowTimeHeld);
                    CrossbowTimeHeld = 0;
                    CrossbowBeingHeld = false;
                    CanFire = false;
                }
            }
        }

        
        if (Input.GetMouseButtonDown(0) && CanFire)
        {
            if (WeaponInformation.HeldDownWeapon == false && AmmoManager.CurrentGunCurrentMag > 0)
            {
                if (WeaponInformation.WeaponName != "Chain Lightning")
                {
                    if (WeaponInformation.WeaponName != "Mind Blast")
                    {
                        AmmoManager.FireShot();
                        CanFire = false;
                        createdBullet = Instantiate(bullet, BulletTransform.transform.position, BulletTransform.transform.rotation);
                    }
                    else
                    {
                        AmmoManager.FireShot();
                        CanFire = false;
                        createdBullet = Instantiate(bullet, BulletTransform.transform.position + Vector3.right * 2f, gun.transform.rotation * Quaternion.Euler(new Vector3(0f, 0f, 90f)));
                    }
                    switch (WeaponInformation.WeaponName)
                    {
                        case "Shotgun":
                            for (int i = 1; i < WeaponInformation.bulletcount; i++)
                            {
                                createdBullet = Instantiate(bullet, BulletTransform.transform.position, BulletTransform.transform.rotation);
                                createdBullet.transform.position = BulletTransform.transform.position;
                                Rigidbody2D bulletRB = createdBullet.GetComponent<Rigidbody2D>();

                                var x = createdBullet.transform.position.x;
                                var y = createdBullet.transform.position.y;

                                if (i % 2 == 0)
                                {
                                    float rotateAngle = 45 + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

                                    var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;
                                    bulletRB.velocity += MovementDirection;
                                }
                                else
                                {
                                    float rotateAngle = -45 + (Mathf.Atan2(y, x) * Mathf.Rad2Deg);

                                    var MovementDirection = new Vector2(Mathf.Cos(rotateAngle * Mathf.Deg2Rad), Mathf.Sin(rotateAngle * Mathf.Deg2Rad)).normalized;
                                    bulletRB.velocity += MovementDirection;
                                }
                            }
                            break;
                    }
                }
                else if(WeaponInformation.WeaponName == "Chain Lightning")
                {
                    AmmoManager.FireShot();
                    RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
                    if (_hit.collider.gameObject.tag == "Enemy")
                    {
                        EnemyScript enemyCode = _hit.collider.gameObject.GetComponent<EnemyScript>();
                        enemyCode.BeenZapped = true;
                        enemyCode.TakeDamage(WeaponInformation.damage);
                        createdBullet = Instantiate(bullet, _hit.collider.gameObject.transform.position, Quaternion.identity);
                        createdBullet.transform.parent = _hit.collider.gameObject.transform;
                        GameObject electricmanage = GameObject.Find("ElectricManagement");
                        ElectricManager removeelectric = electricmanage.AddComponent<ElectricManager>();
                        removeelectric.ActivateDestruction(createdBullet);
                    }
                }
            }
        }
    }
}
