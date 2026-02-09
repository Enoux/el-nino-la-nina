using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    [Header("Viewpoint")]
    public Viewpoint currentView;

    [Header("Transition")]
    public float transitionTime = 0.35f;
    public AnimationCurve ease = AnimationCurve.EaseInOut(0, 0, 1, 1);

    bool isTransitioning = false;
    private Hotspot[] allHotspots;

    void Start() {
        // Snap camera to starting view
        if (currentView != null) {
            transform.position = currentView.transform.position;
            transform.rotation = currentView.transform.rotation;
        }

        Application.targetFrameRate = 15;
    }

    void Awake() {
        // Update all hotspots
        allHotspots = FindObjectsByType<Hotspot>(FindObjectsSortMode.None);
    }

  public bool CanNavigate() {
        return !isTransitioning;
    }

    public void GoTo(Viewpoint target) {
        if (target == null || !target.isActive || isTransitioning)
            return;

        StopAllCoroutines();
        StartCoroutine(TransitionTo(target));
    }

    IEnumerator TransitionTo(Viewpoint target) {
        isTransitioning = true;

        Vector3 startPos = transform.position;
        Quaternion startRot = transform.rotation;

        Vector3 endPos = target.transform.position;
        Quaternion endRot = target.transform.rotation;

        float t = 0f;
        while (t < 1f) {
            t += Time.deltaTime / transitionTime;
            float eased = ease.Evaluate(t);

            transform.position = Vector3.Lerp(startPos, endPos, eased);
            transform.rotation = Quaternion.Slerp(startRot, endRot, eased);

            yield return null;
        }

        transform.position = endPos;
        transform.rotation = endRot;

        currentView = target;
        isTransitioning = false;

        // Update all Hotspots
        foreach (var h in allHotspots) {
            h.UpdateCollider();
        }
    }
}
