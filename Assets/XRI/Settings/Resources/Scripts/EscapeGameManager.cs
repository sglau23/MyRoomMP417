using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class EscapeGameManager : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text statusText;
    public GameObject winObject;
    public GameObject loseObject;

    [Header("Win / Exit")]
    public GameObject exitBlocker;
    public Transform xrOrigin;
    public Transform winTeleportTarget;

    [Header("Timer")]
    public float timeLimitSeconds = 180f;
    private float timeRemaining;

    [Header("Restart (Controller Button)")]
    public bool allowRestartAnytime = true;
    private bool restartWasPressedLastFrame = false;

    // ---------- EXISTING GATES ----------
    private bool[] gates = new bool[3];
    private readonly string[] gateNames = { "Knife", "Keycard", "Bone" };
    

    // ---------- NEW COLLECTIBLES ----------
    [Header("Collectibles")]
    public int totalCollectibles = 5;
    private int collectedCount = 0;
    // Light flashers
    public WinLightFlasher winLightFlasher;

    private bool gameEnded = false;

    void Start()
    {
        timeRemaining = timeLimitSeconds;
        SetActiveSafe(winObject, false);
        SetActiveSafe(loseObject, false);
        UpdateStatusText();
    }

    void Update()
    {
        if (allowRestartAnytime || gameEnded)
        {
            if (RestartPressedThisFrame())
            {
                RestartScene();
                return;
            }
        }

        if (gameEnded) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            Lose();
            return;
        }

        UpdateStatusText();
    }

    // ---------- COLLECTIBLE FUNCTION ----------
    public void CollectItem()
    {
        if (gameEnded) return;

        collectedCount++;
        Debug.Log($"Collected: {collectedCount}/{totalCollectibles}");
        UpdateStatusText();
    }

    // ---------- GATE PROGRESSION ----------
    public void MarkGateComplete(int i)
    {
        if (gameEnded) return;
        if (i < 0 || i > 2) return;
        if (gates[i]) return;

        gates[i] = true;

        int count = GateCount();
        Debug.Log($"Gate complete: {gateNames[i]} ({count}/3)");

        UpdateStatusText();

        if (count == 3)
            Win();
    }

    // ---------- UI ----------
    void UpdateStatusText()
    {
        if (!statusText) return;

        string timerStr = FormatTime(timeRemaining);
        int count = GateCount();

        string knifeLine = gates[0] ? "Evidence Logged: Knife" : "Evidence Logged:";
        string keycardLine = gates[1] ? "Access Granted: Keycard" : "Access Granted:";
        string boneLine = gates[2] ? "Bone Logged: Bone" : "Bone Logged:";

        if (!gameEnded)
        {
            statusText.text =
                "Murder Mystery Escape Room\n" +
                "Goal: Secure 3 pieces of evidence.\n\n" +
                $"{knifeLine}\n" +
                $"{keycardLine}\n" +
                $"{boneLine}\n\n" +
                $"Progress: {count}/3\n" +
                $"Collectibles: {collectedCount}/{totalCollectibles}\n" +
                $"Time Left: {timerStr}\n\n" +
                "Press X (Left) to Restart";
        }
    }

    // ---------- WIN / LOSE ----------
    void Win()
    {
        if (winLightFlasher != null)
        winLightFlasher.StartFlashing();

        if (gameEnded) return;
        gameEnded = true;

        if (exitBlocker) exitBlocker.SetActive(false);
        SetActiveSafe(winObject, true);

        if (statusText)
            statusText.text = "🎉 YOU WIN! 🎉\nAll evidence secured.\n\nPress X (Left) to Restart.";

        if (xrOrigin && winTeleportTarget)
        {
            xrOrigin.position = winTeleportTarget.position;
            xrOrigin.rotation = winTeleportTarget.rotation;
        }
    }

    void Lose()
    {
        if (gameEnded) return;
        gameEnded = true;

        SetActiveSafe(loseObject, true);

        if (statusText)
            statusText.text = "⏳ YOU LOSE! ⏳\nTime ran out.\n\nPress X (Left) to Restart.";
    }

    // ---------- RESTART ----------
    bool RestartPressedThisFrame()
    {
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        bool pressedNow = false;
        if (leftHand.isValid)
            leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out pressedNow);

        bool pressedThisFrame = pressedNow && !restartWasPressedLastFrame;
        restartWasPressedLastFrame = pressedNow;

        return pressedThisFrame;
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // ---------- HELPERS ----------
    int GateCount()
    {
        return (gates[0] ? 1 : 0) + (gates[1] ? 1 : 0) + (gates[2] ? 1 : 0);
    }

    string FormatTime(float t)
    {
        int seconds = Mathf.CeilToInt(t);
        int m = seconds / 60;
        int s = seconds % 60;
        return $"{m:00}:{s:00}";
    }

    void SetActiveSafe(GameObject go, bool active)
    {
        if (go) go.SetActive(active);
    }
}