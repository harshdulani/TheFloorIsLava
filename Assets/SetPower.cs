using UnityEngine;
using UnityEngine.UI;

public class SetPower : MonoBehaviour
{
    public float changePowerSpeed = 0.1f;

    public Image sliderImage;

    private void Start()
    {
        sliderImage.fillAmount = 0f;
    }

    private void Update()
    {
        if (JumpManager.canSetPower)
        {
            if (Input.GetMouseButton(0))
            {
                sliderImage.fillAmount += Input.GetAxis("Mouse Y") * -changePowerSpeed;
            }
            if (sliderImage.fillAmount > 0f)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    JumpManager.setPowerX = sliderImage.fillAmount;
                    sliderImage.fillAmount = 0f;
                    JumpManager.canSetPower = false;
                    if (JumpManager.FLOW_DEBUG)
                        print("canSetPower = " + JumpManager.canSetPower);

                    print("power locked");

                    JumpManager.callJump = true;
                }
            }
        }
    }
}
