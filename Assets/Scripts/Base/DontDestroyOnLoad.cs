using UnityEngine;

/// <summary>
/// 씬 전환 시에도 파괴되지 않는 오브젝트를 위한 클래스
/// </summary>
public class DontDestroyOnLoad : MonoBehaviour
{
    /// <summary>
    /// Awake 메서드에서 오브젝트를 파괴하지 않도록 설정
    /// </summary>
    private void Awake()
    {
        // 씬 전환 시에도 오브젝트가 파괴되지 않도록 설정
        DontDestroyOnLoad(gameObject);
    }
}