using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 모든 이벤트 처리 스크립트에서 구현할 수 있는 인터페이스
 * 
 * OnCompleted: 스크립트에서 처리하는 이벤트가 종료되었을 경우 구현할 내용
 *  - parameter: 다음 스크립트 이벤트에 전달할 정보
 *  - return: 다음 스크립트 클래스
 * 
 */
interface ScriptManagerInterface
{
    void OnCompleted(); 
}
