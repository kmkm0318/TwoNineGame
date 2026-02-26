/// <summary>
/// 게임 일시정지 상태
/// </summary>
public class GamePauseState : GameBaseState
{
    #region 레퍼런스
    private InputManager _inputManager;
    private GamePresenter _gamePresenter;
    private PausePresenter _pausePresenter;
    #endregion

    public GamePauseState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 할당
        _inputManager = InputManager.Instance;
        _gamePresenter = GameManager.GameUIManager.GamePresenter;
        _pausePresenter = GameManager.GameUIManager.PausePresenter;
    }

    public override void OnEnter()
    {
        // 숫자 UI 비활성화
        _gamePresenter.SetNumberUIActive(false);

        // 일시정지 UI 활성화
        _pausePresenter.Show();

        // 이벤트 구독
        RegisterEvents();
    }

    public override void OnExit()
    {
        // 일시정지 UI 비활성화
        _pausePresenter.Hide();

        // 숫자 UI 활성화
        _gamePresenter.SetNumberUIActive(true);

        // 이벤트 구독 해제
        UnregisterEvents();
    }

    public override void OnUpdate()
    {

    }

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        _inputManager.DefaultInput.Player.Back.performed += HandleOnBackPerformed;
    }

    private void UnregisterEvents()
    {
        _inputManager.DefaultInput.Player.Back.performed -= HandleOnBackPerformed;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnBackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // 게임 플레이 상태로 전환
        StateMachine.ChangeState(Factory.PlayingState);
    }
    #endregion
}