using UnityEngine;
using UnityEngine.InputSystem; 

public class Position : MonoBehaviour
{
    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            myLight.color = Color.magenta;
        }
    }
}
