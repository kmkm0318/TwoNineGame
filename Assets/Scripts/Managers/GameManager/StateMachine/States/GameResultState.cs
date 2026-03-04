using System;

/// <summary>
/// 게임 결과 상태
/// </summary>
public class GameResultState : GameBaseState
{
    #region 레퍼런스
    private InputManager _inputManager;
    private RoundManager _roundManager;
    private UserDataManager _userDataManager;
    private ResultPresenter _resultPresenter;
    private ConfirmPresenter _confirmPresenter;
    #endregion

    public GameResultState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _inputManager = InputManager.Instance;
        _roundManager = gameManager.RoundManager;
        _userDataManager = gameManager.UserDataManager;
        _resultPresenter = gameManager.GameUIManager.ResultPresenter;
        _confirmPresenter = gameManager.GameUIManager.ConfirmPresenter;
    }

    public override void OnEnter()
    {
        // BGM 피치 초기화
        AudioManager.Instance.SetBGMPitch(1f);

        // 결과 사운드 재생
        AudioManager.Instance.PlaySFX(SFXType.Game_Result);

        // 점수 가져오기
        var score = _roundManager.CurrentScore;

        // 최고 점수 갱신 시도
        _userDataManager.UpdateBestScore(score);

        // 점수 결과 설정
        _resultPresenter.SetScore(score);

        // 결과 UI 표시
        _resultPresenter.Show();

        // BGM 재생
        AudioManager.Instance.PlayBGM(BGMType.Result);

        // 이벤트 구독
        RegisterEvents();
    }

    public override void OnExit()
    {
        // 결과 UI 즉시 숨기기
        _resultPresenter.Hide(0f);

        // 이벤트 구독 해제
        UnregisterEvents();
    }

    public override void OnUpdate()
    {

    }

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        // 뒤로가기 입력 구독
        _inputManager.DefaultInput.Player.Back.performed += HandleOnBackPerformed;
    }

    private void UnregisterEvents()
    {
        // 뒤로가기 입력 구독 해제
        _inputManager.DefaultInput.Player.Back.performed -= HandleOnBackPerformed;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnBackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        // 뒤로가기 입력이 들어오면 확인 UI 종료 시도
        _confirmPresenter.TryCancel();
    }
    #endregion
}