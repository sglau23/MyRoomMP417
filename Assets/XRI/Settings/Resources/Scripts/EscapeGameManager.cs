using UnityEngine;

public class EscapeGameManager : MonoBehaviour
{
    public GameObject winObject;

    bool[] gates = new bool[3];

    public void MarkGateComplete(int i)
    {
        if (i < 0 || i > 2) return;
        gates[i] = true;

        int count = (gates[0] ? 1 : 0) + (gates[1] ? 1 : 0) + (gates[2] ? 1 : 0);
        Debug.Log("Gates complete: " + count + "/3");

        if (count == 3)
            Win();
    }

    void Win()
    {
        Debug.Log("YOU WIN!");
        if (winObject != null) winObject.SetActive(true);
    }
}