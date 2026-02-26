using UnityEngine;

/// <summary>
/// 게임 홈 상태
/// </summary>
public class GameHomeState : GameBaseState
{
    #region 레퍼런스
    private InputManager _inputManager;
    private HomePresenter _homePresenter;
    #endregion

    public GameHomeState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 할당
        _inputManager = InputManager.Instance;
        _homePresenter = gameManager.GameUIManager.HomePresenter;
    }

    public override void OnEnter()
    {
        // BGM 재생
        AudioManager.Instance.PlayBGM(BGMType.Home);

        // 홈 UI 즉시 표시
        _homePresenter.Show(0f);

        // 이벤트 구독
        RegisterEvents();
    }

    public override void OnExit()
    {
        // 홈 UI 즉시 숨기기
        _homePresenter.Hide(0f);

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
        // 애플리케이션 종료
        Application.Quit();
    }
    #endregion
}