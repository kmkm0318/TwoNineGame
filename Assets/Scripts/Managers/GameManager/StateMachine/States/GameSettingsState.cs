/// <summary>
/// 게임 설정 상태
/// </summary>
public class GameSettingsState : GameBaseState
{
    #region 레퍼런스
    private InputManager _inputManager;
    private SettingsPresenter _settingsPresenter;
    #endregion

    public GameSettingsState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 할당
        _inputManager = InputManager.Instance;
        _settingsPresenter = GameManager.GameUIManager.SettingsPresenter;
    }

    public override void OnEnter()
    {
        // 설정 UI 활성화
        _settingsPresenter.Show();

        // 이벤트 구독
        RegisterEvents();
    }

    public override void OnExit()
    {
        // 설정 UI 비활성화
        _settingsPresenter.Hide();

        // 이벤트 구독 해제
        UnregisterEvents();
    }

    public override void OnUpdate()
    {

    }

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        // 인풋 액션 가져오기
        var inputAction = _inputManager.DefaultInput;

        // Back 액션 구독
        inputAction.Player.Back.performed += HandleOnBackPerformed;
    }

    private void UnregisterEvents()
    {
        // 인풋 액션 가져오기
        var inputAction = _inputManager.DefaultInput;

        // Back 액션 구독 해제
        inputAction.Player.Back.performed -= HandleOnBackPerformed;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnBackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // 홈 상태로 전환
        StateMachine.ChangeState(Factory.HomeState);
    }
    #endregion
}