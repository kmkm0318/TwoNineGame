/// <summary>
/// 게임 결과 상태
/// </summary>
public class GameResultState : GameBaseState
{
    #region 레퍼런스
    private RoundManager _roundManager;
    private UserDataManager _userDataManager;
    private ResultPresenter _resultPresenter;
    #endregion

    public GameResultState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory) : base(gameManager, stateMachine, factory)
    {
        // 레퍼런스 설정
        _roundManager = gameManager.RoundManager;
        _userDataManager = gameManager.UserDataManager;
        _resultPresenter = gameManager.GameUIManager.ResultPresenter;
    }

    public override void OnEnter()
    {
        // 결과 사운드 재생
        AudioManager.Instance.PlaySFX(SFXType.Game_Result);

        // 점수 계산
        var score = _roundManager.CurrentRound - 1;

        // 최고 점수 갱신 시도
        _userDataManager.UpdateHighScore(score);

        // 점수 결과 설정
        _resultPresenter.SetScore(score);

        // 결과 UI 표시
        _resultPresenter.Show();
    }

    public override void OnExit()
    {
        // 결과 UI 즉시 숨기기
        _resultPresenter.Hide(0f);
    }

    public override void OnUpdate()
    {

    }
}