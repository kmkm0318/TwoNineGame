/// <summary>
/// 게임 라운드 시작 상태
/// </summary>
public class GameRoundStartState : GameBaseState
{
    #region 레퍼런스
    private RoundManager _roundManager;
    private NumberManager _numberManager;
    #endregion

    public GameRoundStartState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _roundManager = gameManager.RoundManager;
        _numberManager = gameManager.NumberManager;
    }

    public override void OnEnter()
    {
        // 목표 배수 증가
        _numberManager.IncreaseTargetMultiple();

        // 새로운 숫자 생성
        _numberManager.GenerateNumbers();

        // 다음 라운드 시작
        _roundManager.StartNextRound();

        // 점수 가져오기
        var score = _roundManager.CurrentScore;

        // 피치 계산
        var pitch = AudioManager.Instance.GetPitchByScore(score);

        // BGM 피치 설정
        AudioManager.Instance.SetBGMPitch(pitch);

        // 게임 플레이 상태로 전환
        StateMachine.ChangeState(Factory.PlayingState);
    }

    public override void OnExit()
    {

    }

    public override void OnUpdate()
    {

    }
}