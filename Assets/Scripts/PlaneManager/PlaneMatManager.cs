using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneMatManager : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager m_ARPlaneManager;
    [SerializeField]
    private GameObject planeWithShadow;
    [SerializeField]
    private GameObject planeWithShadowAndVisualizer;

    bool isPressed;
    Material planeMaterial;

    ARPlaneMeshVisualizerOverride m_ARPlaneMeshVisualizerOverride;

    // Start is called before the first frame update
    void Start()
    {
        m_ARPlaneMeshVisualizerOverride = planeWithShadowAndVisualizer.GetComponent<ARPlaneMeshVisualizerOverride>();
        Debug.Log("m_ARPlaneMeshVisualizerOverride: " + m_ARPlaneMeshVisualizerOverride);
        if (m_ARPlaneMeshVisualizerOverride != null)
        {
            m_ARPlaneMeshVisualizerOverride.toggleFlag = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            Debug.Log("isPressed true");
        }
        else
        {
            Debug.Log("isPressed false");
        }
    }

    public void OnPress()
    {
        isPressed = true;
        m_ARPlaneMeshVisualizerOverride.toggleFlag = false;

        Debug.Log("OnPress");
    }

    public void OnRelease()
    {
        isPressed = false;
        m_ARPlaneMeshVisualizerOverride.toggleFlag = true;

        Debug.Log("OnRelease");
    }
}
