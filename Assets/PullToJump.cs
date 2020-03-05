using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullToJump : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;

    //calculate throw distance
    public float forceMultiplier = 500f, yForceMultiplier = 1.5f, minForce, maxForce;

    private Vector3 startPos, tempPos, endPos;
    private bool canJump = false;

    //calculate rotation
    private Vector3 direction, playerPos;
    private float angle;

    //original & current trail size
    public GameObject trailHolder;
    public float trailSize_scaleMultiplier = 2f;

    private Vector3 trailSize_original;

    //testing
    public bool applyForce = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        trailSize_original = trailHolder.transform.localScale;

        minForce *= forceMultiplier;
        maxForce *= forceMultiplier;
    }

    private void Update()
    {
        //only jump if player is on ground/furniture/ "SafeToStandOn"
        if (canJump)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                print("start position = " + startPos);
            }
            if (Input.GetMouseButton(0))
            {
                tempPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);
                var distanceCalc = Mathf.Sqrt((Mathf.Pow(tempPos.x - startPos.x, 2f) + Mathf.Pow(tempPos.y - startPos.y, 2f)));


                //dont rotate camera for if mouse pointer is in front of player
                if (startPos.y > tempPos.y)
                {
                    //print("should rotate");

                    //rotate the player while dragging
                    direction = mainCamera.WorldToViewportPoint(transform.position) - tempPos;
                    angle = (Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg) - 90f;
                    angle = Mathf.Clamp(angle, -70f, 70f);
                    transform.rotation = Quaternion.AngleAxis(angle, transform.up);

                    //scale trail to Y force
                    trailHolder.transform.localScale =
                        new Vector3(trailSize_original.x,
                        trailSize_original.y,
                        distanceCalc * trailSize_scaleMultiplier);
                }
                else
                {
                    //**maybe slow the rotate rate (lol or maybe introduce a rotate rate)
                    //print("shouldnt rotate");
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                endPos = mainCamera.ScreenToViewportPoint(Input.mousePosition);

                //calculate the distance to be thrown.
                var distanceCalc = Mathf.Sqrt((Mathf.Pow(endPos.x - startPos.x, 2f) + Mathf.Pow(endPos.y - startPos.y, 2f)));

                //print("end position = " + endPos);
                //print("distance is = " + distanceCalc);

                //apply equal Y forces in the Y & Z axis
                //higher Y forces than Z is definitely better, because height decreases faster due to gravity
                var forceVector = new Vector3(0f,
                    Mathf.Clamp(distanceCalc * yForceMultiplier * forceMultiplier, minForce * yForceMultiplier, maxForce * yForceMultiplier),
                    Mathf.Clamp(distanceCalc * forceMultiplier, minForce, maxForce));

                //apply force only if force is more than deadzone
                if (applyForce)
                {
                    //apply force only if the pointer is below the player only
                    if (startPos.y > tempPos.y)
                    {
                        rb.AddRelativeForce(forceVector);
                        print("applied force = " + forceVector);
                        //insert haptics here
                    }
                }

                //revert trail to original size
                trailHolder.transform.localScale = trailSize_original;
                canJump = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("SafeToStandOn"))
        {
            canJump = true;
            print("SafeToStandOn");
        }
    }
}
