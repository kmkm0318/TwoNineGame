/// <summary>
/// 게임 라운드 클리어 상태
/// </summary>
public class GameRoundClearState : GameBaseState
{
    #region 레퍼런스
    private NumberManager _numberManager;
    private GamePresenter _gamePresenter;
    #endregion

    public GameRoundClearState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _numberManager = gameManager.NumberManager;
        _gamePresenter = gameManager.GameUIManager.GamePresenter;
    }

    public override void OnEnter()
    {
        // 숫자 버튼의 색 변경 애니메이션 실행
        _gamePresenter.ShowNumberButtonsResultColor(_numberManager.CurrentTargetMultiple, () =>
        {
            // 다음 라운드 시작 상태로 전환
            StateMachine.ChangeState(Factory.RoundStartState);
        });
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}