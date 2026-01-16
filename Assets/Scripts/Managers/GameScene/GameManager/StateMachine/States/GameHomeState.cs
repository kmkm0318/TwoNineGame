/// <summary>
/// 게임 홈 상태
/// </summary>
public class GameHomeState : GameBaseState
{
    #region 레퍼런스
    private HomePresenter _homePresenter;
    #endregion

    public GameHomeState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 할당
        _homePresenter = gameManager.GameUIManager.HomePresenter;
    }

    public override void OnEnter()
    {
        // 홈 UI 표시
        _homePresenter.Show();
    }

    public override void OnExit()
    {
        // 홈 UI 숨기기
        _homePresenter.Hide();
    }

    public override void OnUpdate()
    {

    }
}