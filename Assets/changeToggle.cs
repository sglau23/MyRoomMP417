using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;

public class changeToggle : MonoBehaviour
{
    public Transform roomViewPoint;
    public Transform externalViewPoint;

    private bool isInRoom = true;

    private XRController rightHand;
    private ButtonControl secondaryButton;

    void Start()
    {
        // Get right controller device
        rightHand = InputSystem.GetDevice<XRController>(CommonUsages.RightHand);

        if (rightHand != null)
        {
            secondaryButton =
                rightHand.TryGetChildControl<ButtonControl>("secondaryButton");
        }
    }

    void Update()
    {
        if (secondaryButton != null && secondaryButton.wasPressedThisFrame)
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