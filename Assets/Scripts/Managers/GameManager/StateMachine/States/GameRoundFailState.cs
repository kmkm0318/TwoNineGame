/// <summary>
/// 게임 라운드 실패 상태
/// </summary>
public class GameRoundFailState : GameBaseState
{
    #region 레퍼런스
    private NumberManager _numberManager;
    private GamePresenter _gamePresenter;
    #endregion

    public GameRoundFailState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _numberManager = gameManager.NumberManager;
        _gamePresenter = gameManager.GameUIManager.GamePresenter;
    }

    public override void OnEnter()
    {
        // 라운드 실패 사운드 재생
        AudioManager.Instance.PlaySFX(SFXType.Game_Wrong);

        // 숫자 버튼의 색 변경 애니메이션 실행
        _gamePresenter.ShowNumberButtonsResultColor(_numberManager.CurrentTargetMultiple, () =>
        {
            // 게임 결과 상태로 전환
            StateMachine.ChangeState(Factory.ResultState);
        });
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}