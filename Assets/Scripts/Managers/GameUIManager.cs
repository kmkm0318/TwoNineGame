using UnityEngine;

/// <summary>
/// 게임 씬의 UI 관리를 담당하는 매니저 클래스
/// </summary>
public class GameUIManager : MonoBehaviour
{
    #region 프레젠터
    [Header("UI Presenters")]
    [SerializeField] private GamePresenter _gamePresenter;
    [SerializeField] private HomePresenter _homePresenter;
    [SerializeField] private ResultPresenter _resultPresenter;
    [SerializeField] private SettingsPresenter _settingsPresenter;
    [SerializeField] private TutorialPresenter _tutorialPresenter;
    [SerializeField] private PausePresenter _pausePresenter;
    public GamePresenter GamePresenter => _gamePresenter;
    public HomePresenter HomePresenter => _homePresenter;
    public ResultPresenter ResultPresenter => _resultPresenter;
    public SettingsPresenter SettingsPresenter => _settingsPresenter;
    public TutorialPresenter TutorialPresenter => _tutorialPresenter;

    public PausePresenter PausePresenter => _pausePresenter;
    #endregion

    #region 레퍼런스
    private GameManager _gameManager;
    #endregion

    #region 초기화
    public void Init(GameManager gameManager)
    {
        // 게임 매니저 할당
        _gameManager = gameManager;

        // 프리젠터 초기화
        InitPresenters();
    }

    private void InitPresenters()
    {
        // 각 프리젠터 초기화
        _gamePresenter.Init(_gameManager, _gameManager.RoundManager, _gameManager.NumberManager);
        _homePresenter.Init(_gameManager);
        _resultPresenter.Init(_gameManager);
        _settingsPresenter.Init(_gameManager, _gameManager.SettingsManager);
        _tutorialPresenter.Init();
        _pausePresenter.Init(_gameManager);
    }
    #endregion
}