using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullToJump : MonoBehaviour
{
    private Rigidbody rb;

    //calculate throw distance
    public float forceMultiplier = 10f;

    private Vector3 startPos, tempPos, endPos;

    //calculate rotation
    private Vector3 direction;
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
        trailSize_original = trailHolder.transform.localScale;
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
            //dont rotate camera for if mouse pointer is in front of player
            tempPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            var tempCalc = (startPos - tempPos);
            
            //rotate the player while dragging
            direction = Camera.main.WorldToViewportPoint(transform.position) - tempPos;
            angle = (Mathf.Atan2(direction.y, -direction.x) * Mathf.Rad2Deg) - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, transform.up);

            //scale trail to Y force
            trailHolder.transform.localScale = new Vector3(trailSize_original.x, trailSize_original.y, (trailSize_original.z + tempCalc.y) * trailSize_scaleMultiplier);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //calculate the distance to be thrown.
            var tempCalc = (startPos - endPos);
            //print("end position = " + endPos);
            //print("distance is = " + tempCalc);

            //apply force only if the pointer is below the player only

            //apply equal Y forces in the Y & Z axis
            //experiment with higher Y forces than Z, because height decreases faster due to gravity
            var forceVector = (new Vector3(0f, tempCalc.y, tempCalc.y)) * forceMultiplier;

            //apply force only if force is more than deadzone
            if (applyForce)
            {
                rb.AddRelativeForce(forceVector);
                //insert haptics here
            }
            //print("force vector = " + forceVector);

            //revert trail to original size
            trailHolder.transform.localScale = trailSize_original;
        }
    }
}
