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
    private Script1 ModelSelectManager;
    [SerializeField]
    private Script2 HumanDetectionManager;
    [SerializeField]
    private Script3 ARInteractionManager;
    [SerializeField]
    private Script4 AnimationControlManager;

    // Start is called before the first frame update
    void Start()
    {
        ModelSelectManager.enabled = true;
        HumanDetectionManager.enabled = false;
        ARInteractionManager.enabled = false;
        AnimationControlManager.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    
    }
    
    static void OnCompletedCallback(MonoBehaviour script, System.Object crossScriptInfo)
    {
        script.enabled = false;
        CurrentScript = Tuple.Create(script, crossScriptInfo);
        
        // Make next script enabled true
        
    }
    
    static Tuple<MonoBehaviour, System.Object> OnInitiatedCallback()
    {
        return CurrentScript
    }
}
