using System;
using UnityEngine;

/// <summary>
/// 게임 씬에서 게임의 전반적인 관리를 담당하는 매니저 클래스
/// </summary>
public class GameManager : MonoBehaviour
{
    #region 매니저 레퍼런스
    [Header("Game Scene Managers")]
    [SerializeField] private GameUIManager _gameUIManager;
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private NumberManager _numberManager;
    [SerializeField] private UserDataManager _userDataManager;
    [SerializeField] private SettingsManager _settingsManager;
    public GameUIManager GameUIManager => _gameUIManager;
    public RoundManager RoundManager => _roundManager;
    public NumberManager NumberManager => _numberManager;
    public UserDataManager UserDataManager => _userDataManager;
    public SettingsManager SettingsManager => _settingsManager;
    #endregion

    #region FSM
    private GameStateMachine _gameStateMachine;
    private GameStateFactory _gameStateFactory;
    #endregion

    #region 변수
    public bool IsRetried { get; private set; } = false;
    #endregion

    #region 이벤트
    public event Action OnPauseRequested;
    #endregion

    private void Start()
    {
        // 초기화
        Init();

        // 초기 상태로 전환
        _gameStateMachine.ChangeState(_gameStateFactory.HomeState);
    }

    private void Update()
    {
        // 상태 머신 업데이트
        _gameStateMachine.UpdateState();
    }

    #region 초기화
    private void Init()
    {
        // FSM 초기화
        InitStateMachine();

        // 매니저 초기화
        InitManagers();
    }

    private void InitStateMachine()
    {
        // 상태 머신 및 팩토리 초기화
        _gameStateMachine = new GameStateMachine();
        _gameStateFactory = new GameStateFactory(this, _gameStateMachine);
    }

    private void InitManagers()
    {
        // 각 매니저 초기화
        _roundManager.Init(_userDataManager);
        _numberManager.Init();
        _userDataManager.Init();
        _settingsManager.Init();
        AudioManager.Instance.Init(_settingsManager);
        LocalizationManager.Instance.Init(_settingsManager);

        // UI 매니저는 가장 마지막에 초기화
        _gameUIManager.Init(this);
    }
    #endregion

    #region 상태 전환 함수
    public void StartGame()
    {
        // 재시도 여부 초기화
        IsRetried = false;

        // 게임 시작 상태로 전환
        _gameStateMachine.ChangeState(_gameStateFactory.LoadingState);
    }
    public void ShowSettings() => _gameStateMachine.ChangeState(_gameStateFactory.SettingsState);
    public void ReturnToHome() => _gameStateMachine.ChangeState(_gameStateFactory.HomeState);
    public void ExitGame() => Application.Quit();
    public void RetryGame()
    {
        // 재시도가 불가능하면 패스
        if (!CanRetry()) return;

        // 재시도 상태 설정
        IsRetried = true;

        // 보상형 광고 보여주기
        AdManager.Instance.ShowRewardedAd(() =>
        {
            // 라운드 시작 상태로 전환
            _gameStateMachine.ChangeState(_gameStateFactory.RoundStartState);
        });
    }
    public void PauseGame() => OnPauseRequested?.Invoke();
    public void ResumeGame() => _gameStateMachine.ChangeState(_gameStateFactory.PlayingState);
    #endregion

    #region 기타 함수
    /// <summary>
    /// 재시도 가능 여부 확인
    /// 재시도를 하지 않았고 보상형 광고를 보여줄 수 있을 때 가능
    /// </summary>
    public bool CanRetry() => !IsRetried && AdManager.Instance.CanShowRewardedAd();
    #endregion

    #region 게임 일시중지
    private void OnApplicationPause(bool pauseStatus)
    {
        // 앱이 일시정지 되었을 때
        if (pauseStatus)
        {
            // 일시정지 이벤트 호출
            OnPauseRequested?.Invoke();
        }
    }

    void OnApplicationFocus(bool focus)
    {
        // 앱이 포커스를 잃었을 때
        if (!focus)
        {
            // 일시정지 이벤트 호출
            OnPauseRequested?.Invoke();
        }
    }
    #endregion
}