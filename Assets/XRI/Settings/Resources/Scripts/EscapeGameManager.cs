using UnityEngine;
using TMPro;

public class EscapeGameManager : MonoBehaviour
{
    public TMP_Text statusText;           // drag your Text (TMP) here
    public GameObject winObject;          // optional: YOU WIN canvas
    public GameObject exitBlocker;        // optional: wall/cube to disable
    public Transform xrOrigin;            // optional
    public Transform winTeleportTarget;   // optional

    bool[] gates = new bool[3];

    // Names that show up when each gate is completed
    private readonly string[] gateNames = { "Knife", "Keycard", "Bone" };

    void Start()
    {
        UpdateStatusText();
    }

    public void MarkGateComplete(int i)
    {
        if (i < 0 || i > 2) return;
        if (gates[i]) return;

        gates[i] = true;

        int count = (gates[0] ? 1 : 0) + (gates[1] ? 1 : 0) + (gates[2] ? 1 : 0);
        Debug.Log($"Gate complete: {gateNames[i]} ({count}/3)");

        UpdateStatusText();

        if (count == 3) Win();
    }

void UpdateStatusText()
{
    if (!statusText) return;

    int count = (gates[0] ? 1 : 0) + (gates[1] ? 1 : 0) + (gates[2] ? 1 : 0);

    // Before any gates are done, show a clean start message
    if (count == 0)
    {
        statusText.text =
            "Murder Mystery Escape Room\n" +
            "Goal: Secure 3 pieces of evidence.\n\n" +
            "Progress: 0/3";
        return;
    }

    // After progress starts, show detailed status
    statusText.text =
        $"Evidence Logged: {(gates[0] ? "Knife ✅" : "Knife ❌")}\n" +
        $"Access Granted:  {(gates[1] ? "Keycard ✅" : "Keycard ❌")}\n" +
        $"Bone Logged:     {(gates[2] ? "Bone ✅" : "Bone ❌")}\n\n" +
        $"Progress: {count}/3";
}

    void Win()
    {
        Debug.Log("YOU WIN!");

        if (exitBlocker) exitBlocker.SetActive(false);
        if (winObject) winObject.SetActive(true);

        if (statusText) statusText.text = "🎉 YOU WIN! 🎉\nAll evidence secured.";

        if (xrOrigin && winTeleportTarget)
        {
            xrOrigin.position = winTeleportTarget.position;
            xrOrigin.rotation = winTeleportTarget.rotation;
        }
    }
}
