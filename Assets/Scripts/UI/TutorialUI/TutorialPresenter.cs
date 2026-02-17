using System;
using UnityEngine;

/// <summary>
/// 튜토리얼 UI를 관리하는 프레젠터 클래스
/// </summary>
public class TutorialPresenter : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TutorialUI _tutorialUI;

    #region 변수
    int _currentStepIndex = 0;
    #endregion

    #region 이벤트
    public event Action OnTutorialCompleted;
    #endregion

    private void OnDestroy()
    {
        // 이벤트 해제
        UnregisterEvents();
    }

    #region 초기화
    public void Init()
    {
        // 이벤트 구독
        RegisterEvents();
    }
    #endregion

    #region 이벤트 등록, 해제
    private void RegisterEvents()
    {
        _tutorialUI.OnNextButtonClicked += HandleNextButtonClicked;
    }

    private void UnregisterEvents()
    {
        _tutorialUI.OnNextButtonClicked -= HandleNextButtonClicked;
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleNextButtonClicked()
    {
        // 다음 단계로 이동
        _currentStepIndex++;

        // 해당 단계 UI 표시 시도 성공 시 반환
        if (_tutorialUI.TryShowStep(_currentStepIndex)) return;

        // 모든 단계 완료 시 튜토리얼 종료
        _tutorialUI.Hide();

        // 튜토리얼 완료 이벤트 발생
        OnTutorialCompleted?.Invoke();
    }
    #endregion

    #region 튜토리얼 시작
    public void StartTutorial()
    {
        // 단계 초기화
        _currentStepIndex = 0;

        // 튜토리얼 UI 표시
        _tutorialUI.Show();

        // 첫 단계 UI 표시
        _tutorialUI.TryShowStep(_currentStepIndex);
    }
    #endregion
}