using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class QuickTimeEvent : MonoBehaviour
{

    [SerializeField]
    private Key keyTrigger; // Key to press during QTE
    [SerializeField]
    private RadialProgressComponent timerIndicator;
    [SerializeField]
    private float timer;
    private float timeLeft;
    private bool isActive;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeLeft = timer;
        isActive = false;
        timerIndicator.m_RadialProgress.key = keyTrigger.ToString();
        StartQTE();
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            timeLeft -= Time.deltaTime;

            // Update Radial Bar
            timerIndicator.m_RadialProgress.progress += Time.deltaTime/timer * 100.0f;

            // Debug.Log(timeLeft);
            // Check for success
            if (Keyboard.current[keyTrigger].wasPressedThisFrame)
            {
                QTESuccess();
                isActive = false;
                timeLeft = timer;
            }

            // Check for failure
            else if (timeLeft <= 0)
            {
                QTEFail();
                isActive = false;
                timeLeft = timer;
            }
        }
    }

    void StartQTE()
    {
        isActive = true;
    }

    protected virtual void QTESuccess()
    {
        Debug.Log("Success!");
        // Destroy radial progress UI
        Destroy(timerIndicator.gameObject);
        // timerIndicator = null;
    }
    protected virtual void QTEFail()
    {
        Debug.Log("Fail womp womp");
    }
}
