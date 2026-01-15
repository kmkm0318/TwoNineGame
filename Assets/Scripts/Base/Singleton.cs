using UnityEngine;

/// <summary>
/// 싱글톤 패턴을 구현하는 기본 클래스
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// 싱글톤 인스턴스
    /// </summary>
    public static T Instance { get; private set; }

    /// <summary>
    /// Awake 메서드에서 싱글톤 인스턴스를 설정
    /// 자식 클래스에서 Awake에 추가할 내용이 있을 경우 override하여 사용
    /// </summary>
    protected virtual void Awake()
    {
        if (Instance == null)
        {
            // 싱글톤 인스턴스 설정
            Instance = this as T;
        }
        else
        {
            // 이미 인스턴스가 존재하면 중복된 오브젝트를 파괴
            Destroy(gameObject);
        }
    }
}