using System;
using UnityEngine;

/// <summary>
/// 게임 씬에서 게임의 전반적인 관리를 담당하는 매니저 클래스
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region 매니저 레퍼런스
    [Header("Game Scene Managers")]
    [SerializeField] private GameUIManager _gameUIManager;
    [SerializeField] private RoundManager _roundManager;
    [SerializeField] private NumberManager _numberManager;
    public GameUIManager GameUIManager => _gameUIManager;
    public RoundManager RoundManager => _roundManager;
    public NumberManager NumberManager => _numberManager;
    #endregion

    private void Start()
    {
        // 초기화
        Init();

        // 게임 시작
        StartGame();
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        UnregisterEvents();
    }

    #region 초기화
    private void Init()
    {
        // 매니저 초기화
        InitManagers();

        // 이벤트 구독
        RegisterEvents();
    }

    private void InitManagers()
    {
        // 각 매니저 초기화
        _gameUIManager.Init(this);
        _roundManager.Init(this);
        _numberManager.Init(this);
    }
    #endregion

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        _roundManager.OnRoundCleared += HandleOnRoundCleared;
        _roundManager.OnRoundFailed += HandleOnRoundFailed;
    }

    private void UnregisterEvents()
    {
        _roundManager.OnRoundCleared -= HandleOnRoundCleared;
        _roundManager.OnRoundFailed -= HandleOnRoundFailed;
    }
    #endregion

    #region 이벤트 핸들러
    public void HandleOnRoundCleared()
    {
        // 목표 배수 증가
        _numberManager.IncreaseTargetMultiple();

        // 새로운 숫자 생성
        _numberManager.GenerateNumbers();

        // 다음 라운드 시작
        _roundManager.StartNextRound();
    }

    public void HandleOnRoundFailed()
    {
        // 게임 종료
        EndGame();
    }
    #endregion

    #region 게임 처리
    public void StartGame()
    {
        // 숫자 생성
        _numberManager.GenerateNumbers();

        // 라운드 시작
        _roundManager.StartNextRound();
    }

    public void EndGame()
    {
        // TODO: 게임 종료 및 결과 처리
        $"게임 종료. 최종 라운드: {_roundManager.CurrentRound}".Log();
    }
    #endregion
}