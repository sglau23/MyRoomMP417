using UnityEngine;

public class Position : MonoBehaviour
{
    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            myLight.color = Color.magenta;
        }
    }
}
