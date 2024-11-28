using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolingLaser : MonoBehaviour
{

    [SerializeField] private float defDistanceRay = 100;
    [SerializeField] public GameObject laserFirePoint;
    [SerializeField] public LineRenderer m_lineRenderer;
    Transform m_transform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
        laserFirePoint = GameObject.Find("BulletTransform");
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    void ShootLaser()
    {
        if(Physics2D.Raycast(m_transform.position, transform.right))
        {
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position, transform.right);
            Draw2DRay(laserFirePoint.transform.position, _hit.point);
        }
        else
        {
            Draw2DRay(laserFirePoint.transform.position, laserFirePoint.transform.right * defDistanceRay);
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
        ShootLaser();
    }
}
