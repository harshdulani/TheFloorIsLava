using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullToJump : MonoBehaviour
{
    public float forceMultiplier = 10f;
    public GameObject trail;

    private Rigidbody rb;

    //calculate throw distance
    private Vector3 startPos, tempPos, endPos;

    //calculate rotation
    private Vector3 direction;
    private float angle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //only trigger this is player is on ground/furniture
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //print("start position = " + startPos);
        }
        if(Input.GetMouseButton(0))
        {
            tempPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            var tempCalc = (startPos - tempPos);

            //rotate the player while dragging
            direction = Camera.main.WorldToViewportPoint(transform.position) - tempPos;
            angle = (Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg) - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, transform.up);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //calculate the distance to be thrown.

            var tempCalc = (startPos - endPos);
            //print("end position = " + endPos);
            print("distance is = " + tempCalc);

            //apply force only if the flick is downwards

            //apply equal forces in the Y & Z axis
            var forceVector = (new Vector3(0f, tempCalc.y, tempCalc.y)) * forceMultiplier;



            rb.AddRelativeForce(forceVector);
            print("force vector = " + forceVector);
        }
    }
}
