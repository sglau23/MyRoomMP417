using System.Collections;
using UnityEngine;

public class WinLightFlasher : MonoBehaviour
{
    [Header("Lights to flash")]
    public Light[] lights;

    [Header("Flash settings")]
    public float interval = 0.12f;         // time between color changes
    public float minIntensity = 0.6f;
    public float maxIntensity = 2.5f;
    public bool randomizeIntensity = true;

    private Coroutine routine;
    private bool isFlashing = false;

    public void StartFlashing()
    {
        if (isFlashing) return;
        isFlashing = true;
        routine = StartCoroutine(FlashRoutine());
    }

    public void StopFlashing()
    {
        if (!isFlashing) return;
        isFlashing = false;
        if (routine != null) StopCoroutine(routine);
    }

    IEnumerator FlashRoutine()
    {
        // Safety: if none assigned, try find some in scene
        if (lights == null || lights.Length == 0)
            lights = FindObjectsOfType<Light>();

        while (true)
        {
            for (int i = 0; i < lights.Length; i++)
            {
                if (lights[i] == null) continue;

                lights[i].color = Random.ColorHSV(
                    0f, 1f,   // hue
                    0.8f, 1f, // saturation
                    0.8f, 1f  // value
                );

                if (randomizeIntensity)
                    lights[i].intensity = Random.Range(minIntensity, maxIntensity);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
