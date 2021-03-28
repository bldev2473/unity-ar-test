using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script4 : MonoBehaviour, ScriptManagerInterface
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
        Debug.Log("Script4 Start called.");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
