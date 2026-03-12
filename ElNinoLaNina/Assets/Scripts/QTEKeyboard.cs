using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class QTEKeyboard : MonoBehaviour
{
    [Header("QTE Settings")]
    [SerializeField] private Key keyTrigger;
    [SerializeField] private float timer = 2f;

    [Header("UI")]
    [SerializeField] private RadialProgressComponent timerIndicator;

    [Header("Events")]
    public UnityEvent onSuccess;
    public UnityEvent onFail;

    private float timeLeft;
    private bool isActive;

    void Awake()
    {
        Hide();
    }

    void Update()
    {
        if (!isActive || timerIndicator == null || timerIndicator.m_RadialProgress == null)
            return;

        timeLeft -= Time.deltaTime;

        // Update radial progress
        timerIndicator.m_RadialProgress.progress =
            (1f - timeLeft / timer) * 100f;

        if (Keyboard.current[keyTrigger].wasPressedThisFrame)
        {
            Success();
            EndQTE();
        }

        if (timeLeft <= 0f)
        {
            Fail();
            EndQTE();
        }
    }

    public void StartQTE()
    {
        if (timerIndicator == null) return;

        isActive = true;
        timeLeft = timer;

        timerIndicator.gameObject.SetActive(true);

        timerIndicator.m_RadialProgress.progress = 0;
        timerIndicator.m_RadialProgress.key = keyTrigger.ToString();
    }

    void EndQTE()
    {
        isActive = false;
        Hide();
    }

    void Hide()
    {
        if (timerIndicator != null)
            timerIndicator.gameObject.SetActive(false);
    }

    void Success()
    {
        Debug.Log("QTE Success");
        onSuccess?.Invoke();
    }

    void Fail()
    {
        Debug.Log("QTE Failed");
        onFail?.Invoke();
    }
}