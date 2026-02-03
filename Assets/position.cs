using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;

public class LightToggle : MonoBehaviour
{
    private Light myLight;
    private bool lastPressed = false;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        // Keyboard 
        bool keyboardPressed =
            Keyboard.current != null &&
            Keyboard.current.tabKey.wasPressedThisFrame;

        // Quest Controller 
        bool controllerPressed = false;

        UnityEngine.XR.InputDevice rightHand =
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (rightHand.isValid &&
            rightHand.TryGetFeatureValue(
                UnityEngine.XR.CommonUsages.primaryButton,
                out bool pressed))
        {
            controllerPressed = pressed && !lastPressed;
            lastPressed = pressed;
        }

        // Trigger
        if (keyboardPressed || controllerPressed)
        {
            myLight.color = Color.magenta;
        }
    }
}