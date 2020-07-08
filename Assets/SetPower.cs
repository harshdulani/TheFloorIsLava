using UnityEngine;
using UnityEngine.UI;

public class SetPower : MonoBehaviour
{
    public float changePowerSpeed = 0.1f;
    public float overshootMultiplier = 1.2f;    //this is to overcome current "ideal" jump being super easy

    public Image sliderImage;
    public ArcLineRenderer alr;

    private void Start()
    {
        sliderImage.fillAmount = 0f;
    }

    private void Update()
    {
        if (JumpManager.canSetPower)
        {
            if(!alr.enabled)
                alr.lr.enabled = true;

            if (Input.GetMouseButton(0))
            {
                sliderImage.fillAmount += Input.GetAxis("Mouse Y") * -changePowerSpeed;
                alr.ChangeVelocityValue(sliderImage.fillAmount * overshootMultiplier);
            }
            if (sliderImage.fillAmount > 0f)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    JumpManager.setPowerX = sliderImage.fillAmount * overshootMultiplier;
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
