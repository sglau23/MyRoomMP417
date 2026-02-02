using UnityEngine;
using UnityEngine.XR;

public class changeToggle : MonoBehaviour
{
    public Transform roomViewPoint;
    public Transform externalViewPoint;

    private bool isInRoom = true;

    void Update()
    {
        // Primary button (A / X depending on controller)
        if (InputDevices.GetDeviceAtXRNode(XRNode.RightHand)
            .TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed) && pressed)
        {
            ToggleView();
        }
    }

    void ToggleView()
    {
        if (isInRoom)
        {
            transform.position = externalViewPoint.position;
            transform.rotation = externalViewPoint.rotation;
        }
        else
        {
            transform.position = roomViewPoint.position;
            transform.rotation = roomViewPoint.rotation;
        }

        isInRoom = !isInRoom;
    }
}