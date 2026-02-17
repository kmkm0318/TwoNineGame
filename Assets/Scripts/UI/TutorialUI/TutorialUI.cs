using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 튜토리얼 UI 관리를 담당하는 클래스
/// </summary>
public class TutorialUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _nextButton;
    [SerializeField] private List<GameObject> _tutorialStepPanels;

    #region 이벤트
    public event Action OnNextButtonClicked;
    #endregion

    private void Awake()
    {
        // 다음 버튼 클릭 이벤트 등록
        _nextButton.onClick.AddListener(() => OnNextButtonClicked?.Invoke());
    }

    #region UI 설정
    public bool TryShowStep(int stepIndex)
    {
        // 모든 패널 숨기기
        foreach (var panel in _tutorialStepPanels)
        {
            panel.SetActive(false);
        }

        // 유효한 인덱스가 아니면 실패
        if (stepIndex < 0 || stepIndex >= _tutorialStepPanels.Count) return false;

        // 해당 단계 패널 활성화
        _tutorialStepPanels[stepIndex].SetActive(true);

        // 성공
        return true;
    }
    #endregion

    #region Show, Hide
    public void Show() => gameObject.SetActive(true);
    public void Hide() => gameObject.SetActive(false);
    #endregion
}