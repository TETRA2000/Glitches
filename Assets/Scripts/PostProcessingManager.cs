using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    private Volume volume;
    public float minGlitchInterval;
    public float maxGlitchInterval;

    private float nextGlitchInteval;
    private float lastGlitchTime;

    private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();

        lastGlitchTime = Time.timeSinceLevelLoad;
        nextGlitchInteval = NextGlitchInterval();

        volume.profile.TryGet<Vignette>(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad - lastGlitchTime > nextGlitchInteval)
        {
            SwitchGlitchMode();

            lastGlitchTime = Time.timeSinceLevelLoad;
            nextGlitchInteval = NextGlitchInterval();
        }
    }

    private void SwitchGlitchMode()
    {
        var mode = Random.Range(0, 2);

        ResetGlitch();

        switch (mode)
        {
            case 0:
                MediumVignette();
                break;
            case 1:
                HeavyVignette();
                break;
            default:
                // Do nothing
                break;
        }
    }

    private float NextGlitchInterval()
    {
        return Random.Range(minGlitchInterval, maxGlitchInterval);
    }

    private void ResetGlitch()
    {
        Debug.Log("ResetGlitch");

        vignette.center.overrideState = false;
        vignette.intensity.overrideState = false;
        vignette.active = false;
    }

    private void MediumVignette()
    {
        Debug.Log("MediumVignette");

        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0.5f;

        vignette.center.overrideState = true;
        vignette.center.value = new Vector2(0.0f, 0.5f);

        vignette.active = true;
    }

    private void HeavyVignette()
    {
        Debug.Log("HeavyVignette");

        vignette.intensity.overrideState = true;
        vignette.intensity.value = 0.7f;
        vignette.active = true;
    }
}
