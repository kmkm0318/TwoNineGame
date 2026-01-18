/// <summary>
/// 게임 상태의 기본 클래스
/// </summary>
public abstract class GameBaseState : IState
{
    #region 레퍼런스
    protected GameManager GameManager { get; private set; }
    protected GameStateMachine StateMachine { get; private set; }
    protected GameStateFactory Factory { get; private set; }
    #endregion

    /// <summary>
    /// 생성자
    /// </summary>
    public GameBaseState(GameManager gameManager, GameStateMachine stateMachine, GameStateFactory factory)
    {
        GameManager = gameManager;
        StateMachine = stateMachine;
        Factory = factory;
    }

    #region 상태 함수
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
    #endregion
}