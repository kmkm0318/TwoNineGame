using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// 디버그 관련 확장 메서드를 제공하는 유틸리티 클래스
/// [Conditional("UNITY_EDITOR")]를 통해 에디터에서만 로그가 출력되도록 함
/// [HideInCallstack]을 통해 로그를 클릭했을 때 호출한 위치로 이동하도록 함
/// </summary>
public static class DebugExtensions
{
    #region 상수
    private const string DEBUG_SYMBOL = "UNITY_EDITOR";
    #endregion

    /// <summary>
    /// 디버그 로그 출력
    /// </summary>
    [Conditional(DEBUG_SYMBOL)]
    [HideInCallstack]
    public static void Log(this object message, Object context = null)
    {
        Debug.Log(message, context);
    }

    /// <summary>
    /// 경고 로그 출력
    /// </summary>
    [Conditional(DEBUG_SYMBOL)]
    [HideInCallstack]
    public static void LogWarning(this object message, Object context = null)
    {
        Debug.LogWarning(message, context);
    }

    /// <summary>
    /// 에러 로그 출력
    /// </summary>
    [Conditional(DEBUG_SYMBOL)]
    [HideInCallstack]
    public static void LogError(this object message, Object context = null)
    {
        Debug.LogError(message, context);
    }
}