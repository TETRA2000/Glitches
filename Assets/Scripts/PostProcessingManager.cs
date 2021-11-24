using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    private Volume volume;

    private Vignette vignette;
    private LensDistortion lensDistortion;
    private ChromaticAberration chromaticAberration;

    private float lastGlitchTransitonTime;
    private enum GlitchState { None, Step1, Step2, Step3 };
    private GlitchState glitchState;

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>();

        volume.profile.TryGet<Vignette>(out vignette);
        volume.profile.TryGet<LensDistortion>(out lensDistortion);
        volume.profile.TryGet<ChromaticAberration>(out chromaticAberration);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGlitchState();
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
                    //HeavyVignette();
                    MediumChromaticAberration();
                    ZoomedInLensDistortion();
                    glitchState = GlitchState.Step2;
                    break;
                case GlitchState.Step2:
                    //MediumVignette();
                    HeavyChromaticAberration();
                    glitchState = GlitchState.Step3;
                    break;
                case GlitchState.Step3:
                    //ResetVignette();
                    ResetLensDistortion();
                    ResetChromaticAberration();
                    glitchState = GlitchState.None;
                    break;
            }
        }
    }

    private void ResetChromaticAberration()
    {
        chromaticAberration.intensity.value = 0.35f;
        chromaticAberration.active = true;
    }

    private void MediumChromaticAberration()
    {
        chromaticAberration.intensity.value = 0.75f;
        chromaticAberration.active = true;
    }

    private void HeavyChromaticAberration()
    {
        chromaticAberration.intensity.value = 1f;
        chromaticAberration.active = true;
    }

    private void ResetLensDistortion()
    {
        lensDistortion.intensity.value = -0.5f;
        lensDistortion.xMultiplier.value = 0.5f;
        lensDistortion.yMultiplier.value = 0.5f;
        lensDistortion.center.value = new Vector2(0.5f, 0.5f);
        lensDistortion.scale.value = 1.18f;
        lensDistortion.active = true;
    }

    private void ZoomedInLensDistortion()
    {
        ResetLensDistortion();
        lensDistortion.scale.value = 3.0f;
    }


    private void ResetVignette()
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
