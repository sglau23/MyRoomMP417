using UnityEngine;
using UnityEngine.XR;

public class changeToggle : MonoBehaviour
{
    public Transform roomViewPoint;
    public Transform externalViewPoint;

    private bool isInRoom = true;
    private bool lastPressed = false;

    void Update()
    {
        InputDevice rightHand =
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        if (!rightHand.isValid)
            return;

        // Button B = secondaryButton on right controller
        if (rightHand.TryGetFeatureValue(
            CommonUsages.secondaryButton, out bool pressed))
        {
            // Edge detection (press once)
            if (pressed && !lastPressed)
            {
                ToggleView();
            }

            lastPressed = pressed;
        }
    }

    void ToggleView()
    {
        if (isInRoom)
        {
            transform.SetPositionAndRotation(
                externalViewPoint.position,
                externalViewPoint.rotation);
        }
        else
        {
            transform.SetPositionAndRotation(
                roomViewPoint.position,
                roomViewPoint.rotation);
        }

        isInRoom = !isInRoom;
    }
}