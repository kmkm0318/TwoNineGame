/// <summary>
/// 게임 상태 팩토리
/// </summary>
public class GameStateFactory
{
    #region 레퍼런스
    private GameManager _gameManager;
    private GameStateMachine _stateMachine;
    #endregion

    #region 상태
    public GameHomeState HomeState { get; private set; }
    public GameLoadingState LoadingState { get; private set; }
    public GameRoundStartState RoundStartState { get; private set; }
    public GamePlayingState PlayingState { get; private set; }
    public GamePauseState PauseState { get; private set; }
    public GameRoundClearState RoundClearState { get; private set; }
    public GameRoundFailState RoundFailState { get; private set; }
    public GameResultState ResultState { get; private set; }
    #endregion

    public GameStateFactory(GameManager gameManager, GameStateMachine stateMachine)
    {
        // 레퍼런스 할당
        _gameManager = gameManager;
        _stateMachine = stateMachine;

        // 상태 인스턴스 생성
        HomeState = new GameHomeState(_gameManager, _stateMachine, this);
        LoadingState = new GameLoadingState(_gameManager, _stateMachine, this);
        RoundStartState = new GameRoundStartState(_gameManager, _stateMachine, this);
        PlayingState = new GamePlayingState(_gameManager, _stateMachine, this);
        RoundClearState = new GameRoundClearState(_gameManager, _stateMachine, this);
        PauseState = new GamePauseState(_gameManager, _stateMachine, this);
        RoundFailState = new GameRoundFailState(_gameManager, _stateMachine, this);
        ResultState = new GameResultState(_gameManager, _stateMachine, this);
    }
}