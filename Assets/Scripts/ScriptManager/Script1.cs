using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script1 : MonoBehaviour, ScriptManagerInterface
{
    System.Object CrossScriptInfo;

    public void OnCompleted()
    {
        CrossScriptInfo = "Script1 Info";
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
        OnInitiated();
        OnCompleted();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
