using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpManager : MonoBehaviour
{
    public static float setPowerX = 0f;
    public float maximumJumpForce = 7.5f;
    public float forceMultiplier = 7.5f;

    //better jumping
    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2f;

    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 forceVector;

    //flow control
    public static bool FLOW_DEBUG = true;
    public static bool canJump = false;
    public static bool canRotate = false;
    public static bool canSetPower = false;
    public static bool callJump = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(callJump)
        {
            Jump();
            callJump = false;
        }
        if (rb.velocity.y < -1f)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            print("falling");
        }
        else if (rb.velocity.y > 1f && !Input.GetMouseButton(0))
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
            print("rising");
        }
    }

    public void Jump()
    {
        /*force based jump
        //calc force vector
        forceVector = (transform.up * maximumJumpForce * setPowerX) + (transform.forward * maximumJumpForce * setPowerX);

        print(forceVector);

        //apply force only if flick started from mid Y of screen
        rb.AddForce(forceVector * forceMultiplier);
        */

        //MAKESHIFT VELOCITY BASED JUMP
        forceVector = (transform.up * maximumJumpForce * setPowerX) + (transform.forward * maximumJumpForce * setPowerX);

        rb.velocity = forceVector * forceMultiplier;

        if (FLOW_DEBUG)
            print("Jump");

        canJump = false;
        forceVector = Vector3.zero;
        setPowerX = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("SafeToStandOn"))
        {
            canJump = true;
            canRotate = true;
            print("SafeToStandOn");
            if (FLOW_DEBUG)
            {
                print("canJump = " + canJump);
                print("canRotate = " + canRotate);
                print("canSetPower = " + canSetPower);
            }
        }
        if (collision.gameObject.CompareTag("Lava"))
        {
            print("you dead, bitch");
        }
    }
}
