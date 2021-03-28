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

    public delegate Tuple<MonoBehaviour, System.Object> ScriptCompletedAction();
    public static event ScriptCompletedAction OnCompleted;

    public static Tuple<MonoBehaviour, System.Object> CurrentScript;

    [SerializeField]
    private Script1 Script1;
    [SerializeField]
    private Script2 Script2;
    [SerializeField]
    private Script3 Script3;
    [SerializeField]
    private Script4 Script4;

    private static Hashtable listTable = new Hashtable();

    // Start is called before the first frame update
    void Start()
    {
        listTable.Add("Script1", new List<MonoBehaviour> { Script2 } );
        listTable.Add("Script2", new List<MonoBehaviour> { Script3, Script4 });

        Script1.enabled = true;
        Script2.enabled = false;
        Script3.enabled = false;
        Script4.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    public static void OnCompletedCallback(MonoBehaviour script, System.Object crossScriptInfo)
    {
        script.enabled = false;
        CurrentScript = Tuple.Create(script, crossScriptInfo);

        // Make next scripts enabled true
        var scriptName = script.GetType().Name as string;
        List<MonoBehaviour> scriptList = (List<MonoBehaviour>)listTable[scriptName];

        foreach (var nextScript in scriptList)
        {
            nextScript.enabled = true;
        }
    }

    public static Tuple<MonoBehaviour, System.Object> OnInitiatedCallback()
    {
        return CurrentScript;
    }
}
