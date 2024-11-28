using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossbowScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider;
    private float weaponDamage;

    public GameObject gun;

    private GameObject grenade;

    public void StartThrow(float distanceThrown)
    {
        gun = GameObject.Find("Gun");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos;
        rigidbody.AddForce(gun.transform.right * 0.001f, ForceMode2D.Impulse);
        StartCoroutine(ActivateTimer(distanceThrown));
    }

    private IEnumerator ActivateTimer(float distanceThrown)
    {
        yield return new WaitForSecondsRealtime(distanceThrown);
        Destroy(transform.gameObject);

    }
}
