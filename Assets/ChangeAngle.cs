using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAngle : MonoBehaviour
{
    [Header("Decide Angle")]
    public float rotateSpeed = 2f;
    public float invertMultiplier = -1f;

    private float yaw;

    private void Update()
    {
        if (JumpManager.canRotate)
        {
            if (Input.GetMouseButton(0))
            {
                yaw += Input.GetAxis("Mouse X") * rotateSpeed;
                transform.parent.eulerAngles = new Vector3(0f, invertMultiplier * yaw, 0f);
            }
            if(Input.GetMouseButtonUp(0))
            {
                JumpManager.canRotate = false;
                if (JumpManager.FLOW_DEBUG)
                    print("canRotate = " + JumpManager.canRotate);

                JumpManager.canSetPower = true;
                if (JumpManager.FLOW_DEBUG)
                    print("canSetPower = " + JumpManager.canSetPower);
            }
        }
    }
}
