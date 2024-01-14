using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System;
using System.Collections;

public class VolumeExpoLerper : MonoBehaviour
{
    public Volume volume;
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        InitializeColorAdjustments();
    }

    private void InitializeColorAdjustments()
    {
        if (!volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments = volume.profile.Add<ColorAdjustments>();
        }
    }

    public void ChangeFromTo(float startValue, float endValue, float duration, Action onComplete = null)
    {
        InitializeColorAdjustments();
        StartCoroutine(LerpExposure(startValue, endValue, duration, onComplete));
    }

    private IEnumerator LerpExposure(float startValue, float endValue, float duration, Action onComplete)
    {
        float timer = 0f;
        float currentValue = colorAdjustments.postExposure.value;

        while (timer < duration)
        {
            float normalizedTime = timer / duration;
            float lerpedValue = Mathf.Lerp(startValue, endValue, normalizedTime);
            colorAdjustments.postExposure.value = lerpedValue;

            timer += Time.deltaTime;
            yield return null;
        }

        colorAdjustments.postExposure.value = endValue;

        onComplete?.Invoke();
    }
}
