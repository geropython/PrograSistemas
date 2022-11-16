using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerEffect : MonoBehaviour
{
    public new Light light;
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    public static float sharedFlicker;
    [Range(1, 50)]
    public int smoothing = 5;
    Queue<float> smoothQueue;
    float lastSum = 0;

    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }
    private void Start()
    {
        smoothQueue = new Queue<float>(smoothing);
        if (light == null)
        {
            light = GetComponent<Light>();
        }
    }
    void Update()
    {
        if (light == null)
        {
            return;
            while(smoothQueue.Count >= smoothing)
            {
                lastSum -= smoothQueue.Dequeue();
            }
            float newVal = Random.Range(minIntensity, maxIntensity);
            smoothQueue.Enqueue(newVal);
            lastSum += newVal;

            // Calculate new smoothed average
            light.intensity = lastSum / (float)smoothQueue.Count;
        }
    }
}
