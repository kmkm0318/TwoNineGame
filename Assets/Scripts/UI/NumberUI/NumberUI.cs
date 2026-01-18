using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 숫자 UI를 담당하는 클래스
/// </summary>
public class NumberUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text _targetMultipleText;
    [SerializeField] private Transform _numberButtonContainer;
    [SerializeField] private NumberButton _numberButtonPrefab;
    [SerializeField] private float _colorChangeDuration = 0.5f;
    [SerializeField] private Ease _colorChangeEase = Ease.Linear;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _correctColor;
    [SerializeField] private Color _wrongColor;

    #region 오브젝트 풀
    private ObjectPool<NumberButton> _numberButtonPool;
    private List<NumberButton> _activeNumberButtons = new();
    #endregion

    #region 이벤트
    public event Action<NumberButton> OnNumberButtonClicked;
    #endregion

    #region 초기화
    public void Init()
    {
        InitPool();
    }

    private void InitPool()
    {
        _numberButtonPool = new(
            () => Instantiate(_numberButtonPrefab, _numberButtonContainer),
            (button) =>
            {
                button.gameObject.SetActive(true);
                _activeNumberButtons.Add(button);
            },
            (button) =>
            {
                button.gameObject.SetActive(false);
                _activeNumberButtons.Remove(button);
            },
            (button) => Destroy(button.gameObject)
        );
    }
    #endregion

    #region UI 업데이트 함수
    public void UpdateTargetMultipleText(int targetMultiple)
    {
        _targetMultipleText.text = $"{targetMultiple}";
    }

    public void UpdateNumberButtons(List<int> numbers)
    {
        // 기존 숫자 버튼 클리어
        ClearNumberButtons();

        foreach (int number in numbers)
        {
            // 버튼 풀에서 숫자 버튼 가져오기
            var numberButton = _numberButtonPool.Get();

            // 색 초기화
            numberButton.SetColor(_defaultColor, 0f);

            // 숫자 초기화
            numberButton.SetNumber(number);

            // 숫자 버튼 클릭 이벤트 구독
            numberButton.OnNumberButtonClicked += HandleOnNumberButtonClicked;
        }
    }

    public void ClearNumberButtons()
    {
        // 활성화된 모든 숫자 버튼 반환
        while (_activeNumberButtons.Count > 0)
        {
            // 버튼 가져오기
            var numberButton = _activeNumberButtons[0];

            // 이벤트 구독 해제
            numberButton.OnNumberButtonClicked -= HandleOnNumberButtonClicked;

            // 버튼 풀에 반환
            _numberButtonPool.Release(numberButton);
        }
    }

    public void ShowNumberButtonsResultColor(int targetMultiple, Action onComplete = null)
    {
        if (_activeNumberButtons.Count == 0)
        {
            // 버튼이 없으면 바로 콜백 호출
            onComplete?.Invoke();

            // 종료
            return;
        }

        // 완료된 버튼 개수 추적
        int completedCount = 0;
        int totalCount = _activeNumberButtons.Count;

        // 완료 콜백 함수
        void onCompleteOnce()
        {
            // 완료된 버튼 개수 증가
            completedCount++;

            if (completedCount >= totalCount)
            {
                // 모든 버튼이 완료되었으면 콜백 호출
                onComplete?.Invoke();
            }
        }

        // 모든 숫자 버튼에 대해 실행
        foreach (var button in _activeNumberButtons)
        {
            // 색 설정
            Color color = button.Number % targetMultiple == 0 ? _correctColor : _wrongColor;

            // 색 변경
            button.SetColor(color, _colorChangeDuration, _colorChangeEase, onCompleteOnce);
        }
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnNumberButtonClicked(NumberButton numberButton)
    {
        // 숫자 버튼 클릭 이벤트 전달
        OnNumberButtonClicked?.Invoke(numberButton);
    }
    #endregion
}