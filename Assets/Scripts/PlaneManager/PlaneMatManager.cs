using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneMatManager : MonoBehaviour
{
    [SerializeField]
    private ARPlaneManager m_ARPlaneManager;

    Material planeMaterial;

    // Start is called before the first frame update
    void Start()
    {
        planeMaterial = m_ARPlaneManager.planePrefab.GetComponent<MeshRenderer>().sharedMaterials[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPress()
    {
        m_ARPlaneManager.planePrefab.GetComponent<MeshRenderer>().sharedMaterials[1] = null;
    }

    public void OnRelease()
    {
        m_ARPlaneManager.planePrefab.GetComponent<MeshRenderer>().sharedMaterials[1] = planeMaterial;
    }
}
