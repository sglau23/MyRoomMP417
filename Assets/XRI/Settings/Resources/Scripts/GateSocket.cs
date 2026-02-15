using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class NewEmptyCSharpScript : MonoBehaviour
{
    public EscapeGameManager gameManager;
    public int gateIndex = 0;
    UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor socket;
    bool triggered = false;
    void Awake()
    {
        socket = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor>();
    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnSelectEntered);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (triggered) return;
        triggered = true;

        if (gameManager != null)
            gameManager.MarkGateComplete(gateIndex);
    }
}
