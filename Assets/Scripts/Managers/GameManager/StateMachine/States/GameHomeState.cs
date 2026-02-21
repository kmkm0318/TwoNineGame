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
        // BGM 재생
        AudioManager.Instance.PlayBGM(BGMType.Home);

        // 홈 UI 즉시 표시
        _homePresenter.Show(0f);

        // 업데이트 확인 코루틴 시작
        GameManager.StartCoroutine(UpdateManager.Instance.CheckForUpdate());
    }

    public override void OnExit()
    {
        // 홈 UI 즉시 숨기기
        _homePresenter.Hide(0f);
    }

    public override void OnUpdate()
    {

    }
}