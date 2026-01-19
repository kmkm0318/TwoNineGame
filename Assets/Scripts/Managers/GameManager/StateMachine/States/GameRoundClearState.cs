/// <summary>
/// 게임 라운드 클리어 상태
/// </summary>
public class GameRoundClearState : GameBaseState
{
    #region 레퍼런스
    private RoundManager _roundManager;
    private NumberManager _numberManager;
    private GamePresenter _gamePresenter;
    #endregion

    public GameRoundClearState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _roundManager = gameManager.RoundManager;
        _numberManager = gameManager.NumberManager;
        _gamePresenter = gameManager.GameUIManager.GamePresenter;
    }

    public override void OnEnter()
    {
        // 라운드 가져오기
        var round = _roundManager.CurrentRound;

        // 피치 계산
        var pitch = 1f + (round - 1) * 0.029f;

        // 라운드 클리어 사운드 재생
        AudioManager.Instance.PlaySFX(SFXType.Game_Correct, pitch, 0f);

        // BGM 피치 조정
        AudioManager.Instance.SetBGMPitch(pitch);

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