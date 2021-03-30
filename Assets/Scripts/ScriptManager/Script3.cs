using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script3 : MonoBehaviour, ScriptManagerInterface
{
    System.Object CrossScriptInfo;

    public void OnCompleted()
    {
        ScriptManager.OnCompletedCallback(this, CrossScriptInfo);
    }

    public void OnInitiated()
    {
        Tuple<string, System.Object> ScriptInfoFrom = ScriptManager.OnInitiatedCallback();
        Debug.Log("Script2 Info: " + ScriptInfoFrom.Item2);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Script3 Start called.");
        OnInitiated();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
