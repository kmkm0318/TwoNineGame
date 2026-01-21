// SteamManager에서 정의된 목표 플랫폼과 동일하게 설정
#if !(UNITY_STANDALONE_WIN || UNITY_STANDALONE_LINUX || UNITY_STANDALONE_OSX || STEAMWORKS_WIN || STEAMWORKS_LIN_OSX)
#define DISABLESTEAMWORKS
#endif

using UnityEngine;

/// <summary>
/// Steamworks.NET을 테스트하기 위한 클래스
/// SteamManager에서 정의된 목표 플랫폼이 아닐 경우에는 클래스 내부를 컴파일하지 않음
/// </summary>
public class SteamTest : MonoBehaviour
{
#if !DISABLESTEAMWORKS
    private void Start()
    {
        if (SteamManager.Initialized)
        {
            var name = Steamworks.SteamFriends.GetPersonaName();
            $"스팀 초기화 성공! 사용자 이름: {name}".Log(this);
        }
        else
        {
            $"스팀 초기화 실패!".LogError(this);
        }
    }
#endif
}