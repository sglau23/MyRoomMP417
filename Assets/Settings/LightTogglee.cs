using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.InputSystem.Controls;

public class LightTogglee : MonoBehaviour
{
    private Light myLight;
    private ButtonControl primaryButton;

    void Start()
    {
        myLight = GetComponent<Light>();

        
    }

    void Update()
    {
        
        myLight.color = Color.red;
        
    }
}