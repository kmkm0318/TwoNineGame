using UnityEngine;

/// <summary>
/// 디버그 관련 확장 메서드를 제공하는 유틸리티 클래스
/// </summary>
public static class DebugExtensions
{
    #region 상수
    private static readonly bool ENABLE_DEBUG = true;
    #endregion

    /// <summary>
    /// 디버그 로그 출력
    /// </summary>
    public static void Log(this string message)
    {
        // 디버그가 활성화되지 않은 경우 패스
        if (!ENABLE_DEBUG) return;

        // 로그 출력
        Debug.Log(message);
    }

    /// <summary>
    /// 경고 로그 출력
    /// </summary>
    public static void LogWarning(this string message)
    {
        // 디버그가 활성화되지 않은 경우 패스
        if (!ENABLE_DEBUG) return;

        // 경고 로그 출력
        Debug.LogWarning(message);
    }

    /// <summary>
    /// 에러 로그 출력
    /// </summary>
    public static void LogError(this string message)
    {
        // 디버그가 활성화되지 않은 경우 패스
        if (!ENABLE_DEBUG) return;

        // 에러 로그 출력
        Debug.LogError(message);
    }
}