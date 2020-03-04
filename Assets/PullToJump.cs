using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullToJump : MonoBehaviour
{
    public float forceMultiplier = 10f;

    private Rigidbody rb;
    private Vector3 startPos, endPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //print("start position = " + startPos);
        }
        if(Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            //print("end position = " + endPos);
            //print("distance is = " + (startPos - endPos));

            var forceVector = new Vector3((startPos - endPos).x, (startPos - endPos).y, (startPos - endPos).y) * forceMultiplier;

            rb.AddForce(forceVector);
            print("force vector = " + forceVector);
        }
    }
}
