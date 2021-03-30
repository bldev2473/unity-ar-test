using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCSSController : MonoBehaviour
{
    public PCSSLight pcssScript;

    private Camera _camera;

	private void Awake()
	{
		SetBlockerSamples(64);

		SetPCFSamples(64);

		SetSoftness(1.5f);

		SetSoftnessFalloff(3.0f);

        SetShadowMode(1);
        SetMSAAMode(0);
	}

	public void SetBlockerSamples(float samplesFloat)
	{
		int samples = Mathf.RoundToInt(samplesFloat);
		pcssScript.Blocker_SampleCount = samples;
		pcssScript.UpdateShaderValues();
	}

	public void SetPCFSamples(float samplesFloat)
	{
		int samples = Mathf.RoundToInt(samplesFloat);
		pcssScript.PCF_SampleCount = samples;
		pcssScript.UpdateShaderValues();
	}

	public void SetSoftness(float softness)
	{
		pcssScript.Softness = softness;
		pcssScript.UpdateShaderValues();
	}

	public void SetSoftnessFalloff(float softnessFalloff)
	{
		pcssScript.SoftnessFalloff = softnessFalloff;
		pcssScript.UpdateShaderValues();
	}

	public void SetShadowMode(int mode)
	{
		switch (mode)
		{
			case (0):
				pcssScript.ResetShadowMode();
				pcssScript._light.shadows = LightShadows.Soft;
				pcssScript.Setup();
				break;
			case (1):
				pcssScript.ResetShadowMode();
				pcssScript._light.shadows = LightShadows.Soft;
				break;
			case (2):
				pcssScript.ResetShadowMode();
				pcssScript._light.shadows = LightShadows.Hard;
				break;
		}
	}

	public void SetMSAAMode(int mode)
	{
		if (!_camera)
			_camera = Camera.main;
		if (!_camera)
			return;

		_camera.allowMSAA = mode > 0;
		switch (mode)
		{
			case (0):
				QualitySettings.antiAliasing = 0;
				break;
			case (1):
				QualitySettings.antiAliasing = 2;
				break;
			case (2):
				QualitySettings.antiAliasing = 4;
				break;
			case (3):
				QualitySettings.antiAliasing = 8;
				break;
		}
	}
}
