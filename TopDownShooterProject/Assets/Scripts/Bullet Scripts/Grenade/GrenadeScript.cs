using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCamera;
    private Rigidbody2D rigidbody;
    private CircleCollider2D circleCollider;
    private float weaponDamage;

    public GameObject gun;
    public GameObject grenadeBlast;

    private GameObject grenade;

    public void StartThrow(float distanceThrown)
    {
        gun = GameObject.Find("Gun");
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidbody = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos;
        rigidbody.AddForce(gun.transform.right * 0.0001f * distanceThrown, ForceMode2D.Impulse);
        StartCoroutine(ActivateFuse());
    }

    private IEnumerator ActivateFuse()
    {
        yield return new WaitForSecondsRealtime(3);
        grenade = Instantiate(grenadeBlast, this.transform.position, Quaternion.identity);
        yield return new WaitForSecondsRealtime(0.1f);
        Destroy(grenade);
        Destroy(transform.gameObject);

    }
}
