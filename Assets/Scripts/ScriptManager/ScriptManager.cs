using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{
    /* 
     * 1. 메인 화면
     * 2. (모델 선택 팝업 -> 모델 선택)
     * 3. (바닥 인식 (가이드 출력) -> 인디케이터 출력 -> 모델 생성 버튼 출력)
     * 4. 모델 생성
     * 5. 사람 인식/터치 인식/애니케이션 컨트롤
     */

    public delegate void OnClickAction();
    public static event OnClickAction OnClick;

    public static Tuple<string, System.Object> CurrentScript;

    //[SerializeField]
    //private Script1 Script1;
    //[SerializeField]
    //private Script2 Script2;
    //[SerializeField]
    //private Script3 Script3;
    //[SerializeField]
    //private Script4 Script4;

    [SerializeField]
    private ModelSelectManager ModelSelectManager;
    [SerializeField]
    private ARKitCoachingOverlay ARKitCoachingOverlay;
    [SerializeField]
    private PlaneDetectionManager PlaneDetectionManager;
    [SerializeField]
    private ARTapToPlaceObject ARTapToPlaceObject;

    private static Hashtable scriptListTable = new Hashtable();

    // Canvas
    [SerializeField]
    private GameObject m_Panel;
    private static GameObject m_ARPanel;

    // Start is called before the first frame update
    void Start()
    {
        //listTable.Add("Script1", new List<MonoBehaviour> { Script2 } );
        //listTable.Add("Script2", new List<MonoBehaviour> { Script3, Script4 });

        //Script1.enabled = true;
        //Script2.enabled = false;
        //Script3.enabled = false;
        //Script4.enabled = false;

        scriptListTable.Add("ModelSelectManager", new List<MonoBehaviour> { ARKitCoachingOverlay });
        scriptListTable.Add("ARKitCoachingOverlay", new List<MonoBehaviour> { ARTapToPlaceObject });

        ModelSelectManager.enabled = true;
        ARKitCoachingOverlay.enabled = false;
        ARTapToPlaceObject.enabled = false;

        m_ARPanel = m_Panel;

        //This outputs what language your system is in
        Debug.Log("This system is in " + Application.systemLanguage);
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    public static void OnCompletedCallback(object script, System.Object crossScriptInfo)
    {
        var scriptName = "";

        if (script is MonoBehaviour)
        {
            MonoBehaviour m_script = (MonoBehaviour)script;
            m_script.enabled = false;
            scriptName = m_script.GetType().Name as string;
        }
        else
        {
            scriptName = script as string;
            if (scriptName == "ARKitCoachingOverlay")
            {
                m_ARPanel.SetActive(true);
            }
        }

        CurrentScript = Tuple.Create(scriptName, crossScriptInfo);

        // Make next scripts enabled true
        List<MonoBehaviour> scriptList = (List<MonoBehaviour>)scriptListTable[scriptName];
        foreach (var nextScript in scriptList)
        {
            nextScript.enabled = true;
        }
    }

    public static Tuple<string, System.Object> OnInitiatedCallback()
    {
        return CurrentScript;
    }
}
