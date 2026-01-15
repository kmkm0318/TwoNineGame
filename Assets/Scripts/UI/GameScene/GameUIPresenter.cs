using UnityEngine;

/// <summary>
/// 게임 씬의 UI 프리젠터 클래스
/// </summary>
public class GameUIPresenter : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private RoundUI _roundUI;
    [SerializeField] private NumberUI _numberUI;

    private RoundManager _roundManager;
    private NumberManager _numberManager;

    private void OnDestroy()
    {
        // 이벤트 해제
        UnregisterEvents();
    }

    #region 초기화
    public void Init(RoundManager roundManager, NumberManager numberManager)
    {
        // 레퍼런스 할당
        _roundManager = roundManager;
        _numberManager = numberManager;

        // 이벤트 등록
        RegisterEvents();

        // UI 초기화
        InitUI();
    }

    private void InitUI()
    {
        _roundUI.UpdateRoundText(_roundManager.CurrentRound);
        _roundUI.UpdateRoundTimeText(_roundManager.CurrentRoundTime);

        _numberUI.Init();
        _numberUI.UpdateTargetMultipleText(_numberManager.CurrentTargetMultiple);
        _numberUI.UpdateNumberButtons(_numberManager.Numbers);
    }
    #endregion

    #region 이벤트 구독, 해제
    private void RegisterEvents()
    {
        _roundManager.OnCurrentRoundChanged += _roundUI.UpdateRoundText;
        _roundManager.OnCurrentRoundTimeChanged += _roundUI.UpdateRoundTimeText;

        _numberManager.OnCurrentTargetMultipleChanged += _numberUI.UpdateTargetMultipleText;
        _numberManager.OnNumbersChanged += _numberUI.UpdateNumberButtons;

        _numberUI.OnNumberButtonClicked += HandleOnNumberButtonClicked;
    }

    private void UnregisterEvents()
    {
        _roundManager.OnCurrentRoundChanged -= _roundUI.UpdateRoundText;
        _roundManager.OnCurrentRoundTimeChanged -= _roundUI.UpdateRoundTimeText;

        _numberManager.OnCurrentTargetMultipleChanged -= _numberUI.UpdateTargetMultipleText;
        _numberManager.OnNumbersChanged -= _numberUI.UpdateNumberButtons;

        _numberUI.OnNumberButtonClicked -= HandleOnNumberButtonClicked;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnNumberButtonClicked(int number)
    {
        // 숫자 정답 여부 확인
        bool success = _numberManager.IsNumberCorrect(number);

        if (success)
        {
            // 라운드 성공
            _roundManager.RoundClear();
        }
        else
        {
            // 라운드 실패
            _roundManager.RoundFail();
        }
    }
    #endregion
}