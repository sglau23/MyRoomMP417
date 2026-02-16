using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GateSocket : MonoBehaviour
{
    public EscapeGameManager gameManager;
    public int gateIndex = 0;

    private XRSocketInteractor socket;
    private bool triggered = false;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
        if (socket == null)
            Debug.LogError("GateSocket: No XRSocketInteractor found on " + gameObject.name);
    }

    void OnEnable()
    {
        if (socket != null)
            socket.selectEntered.AddListener(OnSelectEntered);
    }

    void OnDisable()
    {
        if (socket != null)
            socket.selectEntered.RemoveListener(OnSelectEntered);
    }

    void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (triggered) return;
        triggered = true;

        if (gameManager != null)
            gameManager.MarkGateComplete(gateIndex);
        else
            Debug.LogError("GateSocket: gameManager not assigned on " + gameObject.name);
    }
}

