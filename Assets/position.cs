using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;

public class LightToggle : MonoBehaviour
{
    private Light myLight;
    private ButtonControl primaryButton;

    void Start()
    {
        myLight = GetComponent<Light>();

        var leftHand = InputSystem.GetDevice<XRController>(CommonUsages.LeftHand);
        if (leftHand != null)
        {
            primaryButton = leftHand.TryGetChildControl<ButtonControl>("primaryButton");
        }
    }

    void Update()
    {
        if (primaryButton != null && primaryButton.wasPressedThisFrame)
        {
            myLight.color = Color.magenta;
        }
    }
}