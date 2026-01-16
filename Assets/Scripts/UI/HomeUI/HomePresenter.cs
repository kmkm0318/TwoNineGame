using UnityEngine;

/// <summary>
/// 홈 UI를 담당하는 프리젠터 클래스
/// </summary>
public class HomePresenter : MonoBehaviour, IShowHide
{
    [Header("UI Components")]
    [SerializeField] private HomeUI _homeUI;

    #region 레퍼런스
    private GameManager _gameManager;
    #endregion

    private void OnDestroy()
    {
        // 이벤트 해제
        UnregisterEvents();
    }

    #region 초기화
    public void Init(GameManager gameManager)
    {
        // 레퍼런스 할당
        _gameManager = gameManager;

        // UI 이벤트 구독
        RegisterEvents();

        // UI 초기화
        InitUI();
    }

    private void InitUI()
    {
        // 초기 UI 설정이 필요한 경우 여기에 추가
    }
    #endregion

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        _homeUI.OnStartButtonClicked += HandleStartButtonClicked;
        _homeUI.OnLeaderboardButtonClicked += HandleLeaderboardButtonClicked;
        _homeUI.OnSettingsButtonClicked += HandleSettingsButtonClicked;
        _homeUI.OnExitButtonClicked += HandleExitButtonClicked;
    }

    private void UnregisterEvents()
    {
        _homeUI.OnStartButtonClicked -= HandleStartButtonClicked;
        _homeUI.OnLeaderboardButtonClicked -= HandleLeaderboardButtonClicked;
        _homeUI.OnSettingsButtonClicked -= HandleSettingsButtonClicked;
        _homeUI.OnExitButtonClicked -= HandleExitButtonClicked;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleStartButtonClicked()
    {
        // 게임 시작
        _gameManager.StartGame();
    }

    private void HandleLeaderboardButtonClicked()
    {
        // TODO: 리더보드 UI 표시
    }

    private void HandleSettingsButtonClicked()
    {
        // TODO: 설정 UI 표시
    }

    private void HandleExitButtonClicked()
    {
        // 애플리케이션 종료
        Application.Quit();
    }
    #endregion

    #region Show, Hide
    public void Show()
    {
        _homeUI.Show();
    }

    public void Hide()
    {
        _homeUI.Hide();
    }
    #endregion
}