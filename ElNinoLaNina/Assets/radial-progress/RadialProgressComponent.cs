using MyGameUILibrary;
using UnityEngine;
using UnityEngine.UIElements;

public class RadialProgressComponent : MonoBehaviour
{
    public RadialProgress m_RadialProgress { get; private set; }

    private UIDocument document;

    void OnEnable()
    {
        document = GetComponent<UIDocument>();

        if (document == null)
        {
            Debug.LogError("RadialProgressComponent requires a UIDocument.");
            return;
        }

        var root = document.rootVisualElement;

        // Instead of creating a new radial progress element,
        // find the one defined in the UXML
        m_RadialProgress = root.Q<RadialProgress>("radial-progress");

        if (m_RadialProgress == null)
        {
            Debug.LogError("RadialProgress element not found in UXML. Make sure it has name=\"radial-progress\".");
        }
    }
}