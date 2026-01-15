using UnityEngine;

/// <summary>
/// 게임 씬의 UI 관리를 담당하는 매니저 클래스
/// </summary>
public class GameUIManager : MonoBehaviour
{
    [Header("UI Presenters")]
    [SerializeField] private GameUIPresenter _gameUIPresenter;

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
        // 게임 UI 프리젠터 초기화
        _gameUIPresenter.Init(_gameManager.RoundManager, _gameManager.NumberManager);
    }
    #endregion
}