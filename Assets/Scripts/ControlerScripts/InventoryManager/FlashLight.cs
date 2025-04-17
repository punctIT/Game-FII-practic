using UnityEngine;

public class FlashlightZoom : MonoBehaviour
{
    [Header("Flashlight Settings")]
    public Light flashlight;
    public float rangeStep = 1f;
    public float angleStep = 1f;

    public float minRange = 5f;
    public float maxRange = 50f;
    public float minAngle = 10f;
    public float maxAngle = 80f;

    void Update()
    {
        HandleZoom();
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            flashlight.range = Mathf.Clamp(flashlight.range - rangeStep, minRange, maxRange);
            flashlight.spotAngle = Mathf.Clamp(flashlight.spotAngle + angleStep, minAngle, maxAngle);
        }
        else if (scroll < 0f)
        {
            flashlight.range = Mathf.Clamp(flashlight.range + rangeStep, minRange, maxRange);
            flashlight.spotAngle = Mathf.Clamp(flashlight.spotAngle - angleStep, minAngle, maxAngle);
        }
    }


}
