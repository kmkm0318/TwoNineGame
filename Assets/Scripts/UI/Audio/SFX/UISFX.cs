using UnityEngine;

/// <summary>
/// UI SFX 재생을 담당하는 클래스
/// </summary>
public abstract class UISFX : MonoBehaviour
{
    /// <summary>
    /// SFX 재생 메서드
    /// </summary>
    public void PlaySFX(SFXType sfxType)
    {
        // SFX 타입이 None일 경우 재생하지 않음
        if (sfxType == SFXType.None) return;

        // 오디오 매니저 싱글톤이 없으면 경고 로그 출력 후 종료
        if (AudioManager.Instance == null)
        {
            "AudioManager 인스턴스가 없습니다.".LogWarning(this);
            return;
        }

        // SFX 재생
        AudioManager.Instance.PlaySFX(sfxType);
    }
}