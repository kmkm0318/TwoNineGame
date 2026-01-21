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