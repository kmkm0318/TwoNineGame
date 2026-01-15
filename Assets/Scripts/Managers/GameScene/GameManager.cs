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

    #region 초기화
    private void Start()
    {
        // 매니저 초기화
        InitManagers();

        // 게임 시작
        StartGame();
    }

    private void InitManagers()
    {
        // 각 매니저 초기화
        _gameUIManager.Init(this);
        _roundManager.Init(this);
        _numberManager.Init(this);
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

    #region 라운드 처리
    public void OnRoundCleared()
    {
        // 현재 라운드 종료
        _roundManager.EndCurrentRound();

        // 목표 배수 증가
        _numberManager.IncreaseTargetMultiple();

        // 새로운 숫자 생성
        _numberManager.GenerateNumbers();

        // 다음 라운드 시작
        _roundManager.StartNextRound();
    }

    public void OnRoundFailed()
    {
        // 게임 종료
        EndGame();
    }
    #endregion
}