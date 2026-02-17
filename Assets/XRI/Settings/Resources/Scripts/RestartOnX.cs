using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class RestartOnX : MonoBehaviour
{
    private bool wasPressedLastFrame = false;

    void Update()
    {
        // Get left-hand controller device
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        // X button on Quest = primaryButton on left controller
        bool pressedNow = false;
        if (leftHand.isValid)
        {
            leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out pressedNow);
        }

        // Detect "button down" edge
        if (pressedNow && !wasPressedLastFrame)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        wasPressedLastFrame = pressedNow;
    }
}
