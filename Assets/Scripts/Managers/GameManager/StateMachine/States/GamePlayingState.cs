/// <summary>
/// 게임 플레이 상태
/// </summary>
public class GamePlayingState : GameBaseState
{
    #region 레퍼런스
    private RoundManager _roundManager;
    private NumberManager _numberManager;
    #endregion

    public GamePlayingState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _roundManager = gameManager.RoundManager;
        _numberManager = gameManager.NumberManager;
    }

    public override void OnEnter()
    {
        // 이벤트 구독
        RegisterEvents();

        // 라운드 활성화
        _roundManager.SetRoundActive(true);
    }

    public override void OnExit()
    {
        // 라운드 비활성화
        _roundManager.SetRoundActive(false);

        // 이벤트 구독 해제
        UnregisterEvents();
    }

    public override void OnUpdate()
    {
        // 라운드 타이머는 RoundManager에서 처리
    }

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        _roundManager.OnRoundCleared += HandleOnRoundCleared;
        _roundManager.OnRoundFailed += HandleOnRoundFailed;

        _numberManager.OnCorrectNumberSelected += HandleOnCorrectNumberSelected;
        _numberManager.OnWrongNumberSelected += HandleOnWrongNumberSelected;
    }

    private void UnregisterEvents()
    {
        _roundManager.OnRoundCleared -= HandleOnRoundCleared;
        _roundManager.OnRoundFailed -= HandleOnRoundFailed;

        _numberManager.OnCorrectNumberSelected -= HandleOnCorrectNumberSelected;
        _numberManager.OnWrongNumberSelected -= HandleOnWrongNumberSelected;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnRoundCleared()
    {
        // 라운드 클리어 상태로 전환
        StateMachine.ChangeState(Factory.RoundClearState);
    }

    private void HandleOnRoundFailed()
    {
        // 라운드 실패 상태로 전환
        StateMachine.ChangeState(Factory.RoundFailState);
    }

    private void HandleOnCorrectNumberSelected()
    {
        // 라운드 클리어 처리
        _roundManager.RoundClear();
    }

    private void HandleOnWrongNumberSelected()
    {
        // 라운드 실패 처리
        _roundManager.RoundFail();
    }
    #endregion
}