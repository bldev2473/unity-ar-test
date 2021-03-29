using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script2 : MonoBehaviour, ScriptManagerInterface
{
    System.Object CrossScriptInfo;

    public void OnCompleted()
    {
        CrossScriptInfo = "Script2 Info";
        ScriptManager.OnCompletedCallback(this, CrossScriptInfo);
    }

    public void OnInitiated()
    {
        Tuple<string, System.Object> ScriptInfoFrom = ScriptManager.OnInitiatedCallback();
        Debug.Log("Script1 Info: " + ScriptInfoFrom.Item2);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script2 Start called.");
        OnInitiated();
        OnCompleted();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
