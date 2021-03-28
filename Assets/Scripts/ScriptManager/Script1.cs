using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScriptManager;

public class Script1 : MonoBehaviour, ScriptManagerInterface
{
    System.Object CrossScriptInfo;

    public void OnCompleted()
    {
        ScriptManager.OnCompletedCallback(this, CrossScriptInfo);
    }

    public void OnInitiated()
    {
        ScriptManager.OnInitiatedCallback();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script1 Start called.");
        OnCompleted();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
