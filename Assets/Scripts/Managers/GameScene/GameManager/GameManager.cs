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

    #region FSM
    private GameStateMachine _gameStateMachine;
    private GameStateFactory _gameStateFactory;
    #endregion

    protected override void Awake()
    {
        base.Awake();

        // 초기화
        Init();
    }

    private void Start()
    {
        // 초기 상태로 전환
        _gameStateMachine.ChangeState(_gameStateFactory.HomeState);
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
        _gameUIManager.Init(this);
        _roundManager.Init();
        _numberManager.Init();
    }
    #endregion
}