using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ShadowResolutionController : MonoBehaviour
{
    public Toggle toggle;
    private RenderPipelineAsset rpAsset;
    private UniversalRenderPipelineAsset urpAsset;
    private SROptions sro;

    // Start is called before the first frame update
    void Start()
    {
        rpAsset = UnityEngine.Rendering.GraphicsSettings.renderPipelineAsset;
        urpAsset = (UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset)rpAsset;
        sro = new SROptions();

        //Fetch the Toggle GameObject
        toggle = GetComponent<Toggle>();

        //Add listener for when the state of the Toggle changes, to take action
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });
    }

    // Update is called once per frame
    void ToggleValueChanged(Toggle toggle)
    {
        if (toggle.isOn)
        {
            sro.MainLightShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution._4096;
        }
        else
        {
            sro.MainLightShadowResolution = UnityEngine.Rendering.Universal.ShadowResolution._512;
        }
        
    }
}
