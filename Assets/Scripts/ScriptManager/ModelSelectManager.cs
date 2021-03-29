using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSelectManager : MonoBehaviour, ScriptManagerInterface
{
    System.Object CrossScriptInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCompleted()
    {
        CrossScriptInfo = "Model Info";
        ScriptManager.OnCompletedCallback(this, CrossScriptInfo);
    }

    public void OnInitiated()
    {
        throw new System.NotImplementedException();
    }
}
