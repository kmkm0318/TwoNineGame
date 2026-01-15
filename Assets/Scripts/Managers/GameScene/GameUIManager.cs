using UnityEngine;

/// <summary>
/// 게임 씬의 UI 관리를 담당하는 매니저 클래스
/// </summary>
public class GameUIManager : MonoBehaviour
{
    #region 레퍼런스
    private GameManager _gameManager;
    #endregion

    public void Init(GameManager gameManager)
    {
        // 게임 매니저 할당
        _gameManager = gameManager;
    }
}