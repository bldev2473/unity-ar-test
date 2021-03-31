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
        m_ARPlaneMeshVisualizerOverride.visibility = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            m_ARPlaneMeshVisualizerOverride.visibility = false;
            Debug.Log("isPressed true");
        }
        else
        {
            m_ARPlaneMeshVisualizerOverride.visibility = true;
            Debug.Log("isPressed false");
        }
    }

    public void OnPress()
    {
        Debug.Log("OnPress");
    }

    public void OnRelease()
    {
        Debug.Log("OnRelease");
    }
}
