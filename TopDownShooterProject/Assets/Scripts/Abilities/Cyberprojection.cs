using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Cyberprojection : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Camera MainCamera;
    private Vector3 mousePos;
    private GameObject gun;

    public SoundManager audioManager;

    bool TPReady = true;
    float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        audioManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
        gun = GameObject.Find("Gun");
    }

    // Update is called once per frame
    void Update()
    {
        if (!TPReady)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                TPReady = true;
                timer = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && TPReady)
        {
            CyberProjection();

        }
    }

    public void CyberProjection()
    {
        TPReady = false;
        audioManager.PlaySound("Teleport");
        Vector3 mousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - gun.transform.position).normalized;
        float distanceToMouse = Vector2.Distance(rigidBody.position, mousePos);
        float minMoveDistance = 1.5f;
        RaycastHit2D _hit = Physics2D.Raycast(gun.transform.position, transform.right, 10f);
        if(_hit == false)
        {
            if (distanceToMouse > minMoveDistance)
            {
                Vector2 newPosition = rigidBody.position + direction * 10f;
                rigidBody.MovePosition(newPosition);
            }
        }
    }
}
