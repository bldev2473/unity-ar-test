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

    // Start is called before the first frame update
    void Start()
    {
        m_ARPlaneManager.planePrefab = planeWithShadowAndVisualizer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressed)
        {
            m_ARPlaneManager.planePrefab = planeWithShadow;
            Debug.Log("isPressed true");
        }
        else
        {
            m_ARPlaneManager.planePrefab = planeWithShadowAndVisualizer;
            Debug.Log("isPressed false");
        }
    }

    public void OnPress()
    {
        isPressed = true;
        Debug.Log("OnPress");
    }

    public void OnRelease()
    {
        isPressed = false;
        Debug.Log("OnRelease");
    }
}
