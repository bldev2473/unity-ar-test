#if UNITY_IOS
using System;
using UnityEngine;
using UnityEngine.XR.ARKit;

public class CustomSessionDelegate : DefaultARKitSessionDelegate
{
    protected override void OnCoachingOverlayViewWillActivate(ARKitSessionSubsystem sessionSubsystem)
    {
        Logger.Log(nameof(OnCoachingOverlayViewWillActivate));
    }

    protected override void OnCoachingOverlayViewDidDeactivate(ARKitSessionSubsystem sessionSubsystem)
    {
        Logger.Log(nameof(OnCoachingOverlayViewDidDeactivate));
        ScriptManager.OnCompletedCallback("ARKitCoachingOverlay", "ARKitCoachingOverlay OnDisabled");
    }
}
#endif