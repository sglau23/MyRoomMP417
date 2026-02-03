using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class changeToggle : MonoBehaviour
{
    public Transform roomViewPoint;
    public Transform externalViewPoint;

    private bool isInRoom = true;
    private bool lastXRPressed = false;

    void Update()
    {
        bool toggleRequested = false;

        // XR CONTROLLER (B button / secondary)
        UnityEngine.XR.InputDevice rightHand =
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand);

        bool xrPressed = false;

        if (rightHand.isValid)
        {
            rightHand.TryGetFeatureValue(
                UnityEngine.XR.CommonUsages.secondaryButton,
                out xrPressed);

            if (xrPressed && !lastXRPressed)
            {
                toggleRequested = true;
            }

            lastXRPressed = xrPressed;
        }

        // KEYBOARD (B key, New Input System)
        if (Keyboard.current != null &&
            Keyboard.current.bKey.wasPressedThisFrame)
        {
            toggleRequested = true;
        }

        if (toggleRequested)
        {
            ToggleView();
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