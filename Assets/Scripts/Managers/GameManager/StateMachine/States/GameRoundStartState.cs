/// <summary>
/// 게임 라운드 시작 상태
/// </summary>
public class GameRoundStartState : GameBaseState
{
    #region 레퍼런스
    private UserDataManager _userDataManager;
    private RoundManager _roundManager;
    private NumberManager _numberManager;
    private TutorialPresenter _tutorialPresenter;
    #endregion

    public GameRoundStartState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _userDataManager = gameManager.UserDataManager;
        _roundManager = gameManager.RoundManager;
        _numberManager = gameManager.NumberManager;
        _tutorialPresenter = gameManager.GameUIManager.TutorialPresenter;
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

        // 튜토리얼 완료 여부 가져오기
        var isTutorialCompleted = _userDataManager.UserData.IsTutorialCompleted;

        if (isTutorialCompleted)
        {
            // 튜토리얼 완료일 시 즉시 게임 플레이 상태로 전환
            StateMachine.ChangeState(Factory.PlayingState);
        }
        else
        {
            // 튜토리얼 완료 이벤트 구독
            _tutorialPresenter.OnTutorialCompleted += HandleTutorialCompleted;

            // 튜토리얼 시작
            _tutorialPresenter.StartTutorial();
        }
    }

    private void HandleTutorialCompleted()
    {
        // 튜토리얼 완료 이벤트 해제
        _tutorialPresenter.OnTutorialCompleted -= HandleTutorialCompleted;

        // 사용자 데이터에 튜토리얼 완료 여부 저장
        _userDataManager.SetTutorialCompleted(true);

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