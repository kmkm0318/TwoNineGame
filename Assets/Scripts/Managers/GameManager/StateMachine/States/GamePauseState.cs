/// <summary>
/// 게임 일시정지 상태
/// </summary>
public class GamePauseState : GameBaseState
{
    #region 레퍼런스
    private GamePresenter _gamePresenter;
    private PausePresenter _pausePresenter;
    #endregion

    public GamePauseState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 할당
        _gamePresenter = GameManager.GameUIManager.GamePresenter;
        _pausePresenter = GameManager.GameUIManager.PausePresenter;
    }

    public override void OnEnter()
    {
        // 숫자 UI 비활성화
        _gamePresenter.SetNumberUIActive(false);

        // 일시정지 UI 활성화
        _pausePresenter.Show();
    }

    public override void OnExit()
    {
        // 일시정지 UI 비활성화
        _pausePresenter.Hide();

        // 숫자 UI 활성화
        _gamePresenter.SetNumberUIActive(true);
    }

    public override void OnUpdate()
    {

    }
}