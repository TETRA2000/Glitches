using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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

    private float lastGlitchTransitonTime;
    private enum GlitchState { None, Step1, Step2, Step3 };
    private GlitchState glitchState;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();

        lastGlitchTime = Time.time;
        nextGlitchInteval = NextGlitchInterval();

        volume.profile.TryGet<Vignette>(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGlitchState();

        //SwitchToRandomGlitch();
    }

    private void SwitchToRandomGlitch()
    {
        if (Time.time - lastGlitchTime > nextGlitchInteval)
        {
            SwitchRandomGlitchMode();

            lastGlitchTime = Time.time;
            nextGlitchInteval = NextGlitchInterval();
        }
    }

    public void TriggerGlitch()
    {
        Debug.Log("TriggerGlitch");

        lastGlitchTransitonTime = Time.time;
        glitchState = GlitchState.Step1;
    }

    private void UpdateGlitchState()
    {
        if (glitchState != GlitchState.None && Time.time - lastGlitchTransitonTime > 0.1f)
        {
            lastGlitchTransitonTime = Time.time;

            switch (glitchState)
            {
                case GlitchState.Step1:
                    HeavyVignette();
                    glitchState = GlitchState.Step2;
                    break;
                case GlitchState.Step2:
                    MediumVignette();
                    glitchState = GlitchState.Step3;
                    break;
                case GlitchState.Step3:
                    ResetGlitch();
                    glitchState = GlitchState.None;
                    break;
            }
        }
    }


    private void SwitchRandomGlitchMode()
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
