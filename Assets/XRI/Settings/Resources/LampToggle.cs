using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LampToggle : MonoBehaviour
{
    public Light lampLight;

    UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.selectEntered.AddListener(OnSelect);
    }

    void OnSelect(SelectEnterEventArgs args)
    {
        if (lampLight)
            lampLight.enabled = !lampLight.enabled;
    }
}