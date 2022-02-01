using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    [Tooltip("The time it takes (in seconds) to transition to or from a specific spotlight")]
    public float tweenTime;

    public Light[] globalLights = new Light[4];
    public Light[] stoveLights = new Light[3];
    public Light[] lampLights = new Light[3];
    public Light[] doorLights = new Light[3];
    public Light[] blocksLights = new Light[3];

    private float[] globalLightsIntensities;
    private float[] stoveLightsIntensities;
    private float[] lampLightsIntensities;
    private float[] doorLightsIntensities;
    private float[] blocksLightsIntensities;

    // Start is called before the first frame update
    void Start()
    {
        GameState gameState = GameState.instance;
        gameState.lightController = gameObject.GetComponent<LightController>();

        globalLightsIntensities = new float[globalLights.Length];
        stoveLightsIntensities = new float[stoveLights.Length];
        lampLightsIntensities = new float[lampLights.Length];
        doorLightsIntensities = new float[doorLights.Length];
        blocksLightsIntensities = new float[blocksLights.Length];

        int i = 0;
        foreach(Light light in globalLights)
        {
            globalLightsIntensities[i++] = light.intensity;
        }
        i = 0;
        foreach (Light light in stoveLights)
        {
            stoveLightsIntensities[i++] = light.intensity;
        }
        i = 0;
        foreach (Light light in lampLights)
        {
            lampLightsIntensities[i++] = light.intensity;
        }
        i = 0;
        foreach (Light light in doorLights)
        {
            doorLightsIntensities[i++] = light.intensity;
        }
        i = 0;
        foreach (Light light in blocksLights)
        {
            blocksLightsIntensities[i++] = light.intensity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SnapToStove()
    {
        float t = 0;
        int i = 0;
        foreach (Light light in globalLights)
        {
            light.intensity = globalLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in lampLights)
        {
            light.intensity = lampLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in doorLights)
        {
            light.intensity = doorLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in blocksLights)
        {
            light.intensity = blocksLightsIntensities[i++] * t;
        }
    }

    public IEnumerator TweenToStove(Action onComplete)
    {
        float t = 1.0f;
        int i = 0;
        while (t >= 0)
        {
            t -= Time.deltaTime * (Time.timeScale / tweenTime);

            i = 0;
            foreach(Light light in globalLights)
            {
                light.intensity = globalLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in lampLights)
            {
                light.intensity = lampLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in doorLights)
            {
                light.intensity = doorLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in blocksLights)
            {
                light.intensity = blocksLightsIntensities[i++] * t;
            }
            yield return null;
        }

        SnapToStove();

        onComplete();
        yield return null;
    }

    public IEnumerator TweenFromStove(Action onComplete)
    {
        float t = 0.0f;
        int i = 0;
        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            i = 0;
            foreach (Light light in globalLights)
            {
                light.intensity = globalLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in lampLights)
            {
                light.intensity = lampLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in doorLights)
            {
                light.intensity = doorLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in blocksLights)
            {
                light.intensity = blocksLightsIntensities[i++] * t;
            }
            yield return null;
        }

        t = 1;
        i = 0;
        foreach (Light light in globalLights)
        {
            light.intensity = globalLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in lampLights)
        {
            light.intensity = lampLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in doorLights)
        {
            light.intensity = doorLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in blocksLights)
        {
            light.intensity = blocksLightsIntensities[i++] * t;
        }

        onComplete();
        yield return null;
    }

    public void SnapToLamp()
    {
        float t = 0;
        int i = 0;
        foreach (Light light in globalLights)
        {
            light.intensity = globalLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in stoveLights)
        {
            light.intensity = stoveLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in doorLights)
        {
            light.intensity = doorLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in blocksLights)
        {
            light.intensity = blocksLightsIntensities[i++] * t;
        }
    }

    public IEnumerator TweenToLamp(Action onComplete)
    {
        float t = 1.0f;
        int i = 0;
        while (t >= 0)
        {
            t -= Time.deltaTime * (Time.timeScale / tweenTime);

            i = 0;
            foreach (Light light in globalLights)
            {
                light.intensity = globalLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in stoveLights)
            {
                light.intensity = stoveLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in doorLights)
            {
                light.intensity = doorLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in blocksLights)
            {
                light.intensity = blocksLightsIntensities[i++] * t;
            }
            yield return null;
        }

        SnapToLamp();

        onComplete();
        yield return null;
    }

    public IEnumerator TweenFromLamp(Action onComplete)
    {
        float t = 0.0f;
        int i = 0;
        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            i = 0;
            foreach (Light light in globalLights)
            {
                light.intensity = globalLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in stoveLights)
            {
                light.intensity = stoveLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in doorLights)
            {
                light.intensity = doorLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in blocksLights)
            {
                light.intensity = blocksLightsIntensities[i++] * t;
            }
            yield return null;
        }

        t = 1;
        i = 0;
        foreach (Light light in globalLights)
        {
            light.intensity = globalLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in stoveLights)
        {
            light.intensity = stoveLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in doorLights)
        {
            light.intensity = doorLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in blocksLights)
        {
            light.intensity = blocksLightsIntensities[i++] * t;
        }

        onComplete();
        yield return null;
    }

    public void SnapToDoor()
    {
        float t = 0;
        int i = 0;
        foreach (Light light in globalLights)
        {
            light.intensity = globalLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in lampLights)
        {
            light.intensity = lampLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in stoveLights)
        {
            light.intensity = stoveLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in blocksLights)
        {
            light.intensity = blocksLightsIntensities[i++] * t;
        }
    }

    public IEnumerator TweenToDoor(Action onComplete)
    {
        float t = 1.0f;
        int i = 0;
        while (t >= 0)
        {
            t -= Time.deltaTime * (Time.timeScale / tweenTime);

            i = 0;
            foreach (Light light in globalLights)
            {
                light.intensity = globalLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in lampLights)
            {
                light.intensity = lampLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in stoveLights)
            {
                light.intensity = stoveLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in blocksLights)
            {
                light.intensity = blocksLightsIntensities[i++] * t;
            }
            yield return null;
        }

        SnapToDoor();

        onComplete();
        yield return null;
    }

    public IEnumerator TweenFromDoor(Action onComplete)
    {
        float t = 0.0f;
        int i = 0;
        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            i = 0;
            foreach (Light light in globalLights)
            {
                light.intensity = globalLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in lampLights)
            {
                light.intensity = lampLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in stoveLights)
            {
                light.intensity = stoveLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in blocksLights)
            {
                light.intensity = blocksLightsIntensities[i++] * t;
            }
            yield return null;
        }

        t = 1;
        i = 0;
        foreach (Light light in globalLights)
        {
            light.intensity = globalLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in lampLights)
        {
            light.intensity = lampLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in stoveLights)
        {
            light.intensity = stoveLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in blocksLights)
        {
            light.intensity = blocksLightsIntensities[i++] * t;
        }

        onComplete();
        yield return null;
    }

    public void SnapToBlocks()
    {
        float t = 0;
        int i = 0;
        foreach (Light light in globalLights)
        {
            light.intensity = globalLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in lampLights)
        {
            light.intensity = lampLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in stoveLights)
        {
            light.intensity = stoveLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in doorLights)
        {
            light.intensity = doorLightsIntensities[i++] * t;
        }
    }

    public IEnumerator TweenToBlocks(Action onComplete)
    {
        float t = 1.0f;
        int i = 0;
        while (t >= 0)
        {
            t -= Time.deltaTime * (Time.timeScale / tweenTime);

            i = 0;
            foreach (Light light in globalLights)
            {
                light.intensity = globalLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in lampLights)
            {
                light.intensity = lampLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in stoveLights)
            {
                light.intensity = stoveLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in doorLights)
            {
                light.intensity = doorLightsIntensities[i++] * t;
            }
            yield return null;
        }

        SnapToBlocks();

        onComplete();
        yield return null;
    }

    public IEnumerator TweenFromBlocks(Action onComplete)
    {
        float t = 0.0f;
        int i = 0;
        while (t <= 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / tweenTime);

            i = 0;
            foreach (Light light in globalLights)
            {
                light.intensity = globalLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in lampLights)
            {
                light.intensity = lampLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in stoveLights)
            {
                light.intensity = stoveLightsIntensities[i++] * t;
            }
            i = 0;
            foreach (Light light in doorLights)
            {
                light.intensity = doorLightsIntensities[i++] * t;
            }
            yield return null;
        }

        t = 1;
        i = 0;
        foreach (Light light in globalLights)
        {
            light.intensity = globalLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in lampLights)
        {
            light.intensity = lampLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in stoveLights)
        {
            light.intensity = stoveLightsIntensities[i++] * t;
        }
        i = 0;
        foreach (Light light in doorLights)
        {
            light.intensity = doorLightsIntensities[i++] * t;
        }

        onComplete();
        yield return null;
    }
}
