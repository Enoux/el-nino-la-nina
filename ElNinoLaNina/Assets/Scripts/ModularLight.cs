using UnityEngine;
using System.Collections;
using UnityEngine.Assertions.Comparers;

public class ModularLight : MonoBehaviour, IHoverReceiver, IClickReceiver {
    [Header("Transition")]
    public float transitionTime = 0.35f;
    public AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    bool isTransitioning = false;
    float maxIntensity, maxInnerAngle;
    Light lightComponent;

    void Awake() {
        lightComponent = GetComponent<Light>();
        maxIntensity = lightComponent.intensity;
        maxInnerAngle = lightComponent.innerSpotAngle;
    }

    public void OnHoverEnter() {
        lightComponent.intensity = maxIntensity;
        lightComponent.innerSpotAngle = maxInnerAngle;
        lightComponent.enabled = true;
    }

    public void OnHoverExit() {
        lightComponent.enabled = false;
    }

    public void OnClick() {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        isTransitioning = true;

        float startIntensity = lightComponent.intensity;
        float startInnerAngle = lightComponent.innerSpotAngle;

        float t = 0f;
        while (t < 1f) {
            t += Time.deltaTime / transitionTime;
            float eased = ease.Evaluate(t);

            lightComponent.intensity = Mathf.Lerp(startIntensity, 0, eased);
            lightComponent.innerSpotAngle = Mathf.Lerp(startInnerAngle, 0, eased);

            yield return null;
        }

        lightComponent.intensity = 0;
        lightComponent.innerSpotAngle = 0;
        isTransitioning = false;
    }
}

