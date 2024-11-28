using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rigidBody;

    float horizontalMovement;
    float verticalMovement;
    float speedLimiter = 0.7f;

    public float MoveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (horizontalMovement != 0 && verticalMovement != 0)
        {
            horizontalMovement += speedLimiter;
           verticalMovement *= speedLimiter;
        }

        rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement).normalized * MoveSpeed;
    }
}
