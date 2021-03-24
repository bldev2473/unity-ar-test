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
        ScriptManager.OnCompleted += CustomAction;
    }

    Tuple<MonoBehaviour, System.Object> CustomAction()
    {
        this.enabled = false;
        return Tuple.Create((MonoBehaviour)this, CrossScriptInfo);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
