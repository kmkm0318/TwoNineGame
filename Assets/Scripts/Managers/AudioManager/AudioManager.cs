using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// 오디오 관리를 담당하는 매니저 클래스
/// </summary>
public class AudioManager : Singleton<AudioManager>
{
    #region 상수
    private const string BGM_VOLUME_PARAM = "BGMVolume";
    private const string SFX_VOLUME_PARAM = "SFXVolume";
    #endregion

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _defaultAudioMixer;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _bgmAudioSource;
    [SerializeField] private AudioSource _sfxAudioSource;

    [Header("Audio Clips")]
    [SerializeField] private SerializableDictionary<BGMType, AudioClip> _bgmClips;
    [SerializeField] private SerializableDictionary<SFXType, AudioClip> _sfxClips;

    #region 오디오 재생
    public void PlayBGM(AudioClip bgmClip)
    {
        // 같은 클립이면 재생하지 않음
        if (_bgmAudioSource.clip == bgmClip) return;

        // 클립 설정
        _bgmAudioSource.clip = bgmClip;

        // 재생
        _bgmAudioSource.Play();
    }

    public void PlaySFX(AudioClip sfxClip, float pitch = 1f, float randomPitchRange = 0.1f)
    {
        // 랜덤 피치 적용
        if (randomPitchRange != 0f)
        {
            pitch += Random.Range(-randomPitchRange, randomPitchRange);
        }

        // 피치 적용
        _sfxAudioSource.pitch = pitch;

        // 클립 재생
        _sfxAudioSource.PlayOneShot(sfxClip);
    }
    #endregion

    #region 오디오 설정
    public void SetBGMVolume(float volume) => _defaultAudioMixer.SetFloat(BGM_VOLUME_PARAM, volumeToDecibel(volume));
    public void SetSFXVolume(float volume) => _defaultAudioMixer.SetFloat(SFX_VOLUME_PARAM, volumeToDecibel(volume));
    #endregion

    #region 계산 함수
    private float volumeToDecibel(float volume) => Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
    #endregion
}
