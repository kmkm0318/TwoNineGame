using System.Collections;

#if UNITY_ANDROID && !UNITY_EDITOR
using Google.Play.AppUpdate;
#endif

/// <summary>
/// Google Play의 인 앱 업데이트를 관리하는 매니저 클래스
/// </summary>
public class UpdateManager : Singleton<UpdateManager>
{
#if UNITY_ANDROID && !UNITY_EDITOR
    #region 변수
    private AppUpdateManager _appUpdateManager;
    #endregion

    override protected void Awake()
    {
        base.Awake();

        // AppUpdateManager 초기화
        _appUpdateManager = new();
    }
#endif

    /// <summary>
    /// 업데이트를 확인하고, 업데이트가 가능하면 즉시 업데이트를 시작하는 코루틴
    /// </summary>
    public IEnumerator CheckForUpdate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // 업데이트 정보 요청
        var appUpdateInfo = _appUpdateManager.GetAppUpdateInfo();

        // 업데이트 정보가 준비될 때까지 대기
        yield return appUpdateInfo;

        // 업데이트 정보 요청 결과 확인
        if (appUpdateInfo.Error != AppUpdateErrorCode.NoError)
        {
            // 에러 발생 시 에러 로그
            $"업데이트 에러 발생: {appUpdateInfo.Error}".LogError(this);

            // 코루틴 종료
            yield break;
        }

        // 업데이트 정보 가져오기
        var result = appUpdateInfo.GetResult();

        // 업데이트 가능 여부 확인
        if (result.UpdateAvailability == UpdateAvailability.UpdateAvailable)
        {
            // 즉시 업데이트로 진행
            var updateOptions = AppUpdateOptions.ImmediateAppUpdateOptions();

            // 업데이트 시작
            var startUpdate = _appUpdateManager.StartUpdate(result, updateOptions);

            // 업데이트 대기
            yield return startUpdate;
        }
        else
        {
            // 업데이트가 필요 없는 경우 로그 출력
            $"최신 버전입니다.".Log(this);
        }
#else
        // Android가 아닌 플랫폼에서는 업데이트가 필요 없으므로 로그 출력
        $"업데이트 확인은 Android 플랫폼에서만 지원됩니다.".Log(this);
        yield break;
#endif
    }
}