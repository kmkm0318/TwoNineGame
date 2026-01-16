using System;
using System.Collections.Generic;
using System.Linq;
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

    #region 오브젝트 풀
    private ObjectPool<NumberButton> _numberButtonPool;
    private List<NumberButton> _activeNumberButtons = new();
    #endregion

    #region 이벤트
    public event Action<int> OnNumberButtonClicked;
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
        // 포커스 업데이트가 필요한지 여부 저장
        bool shouldUpdateFocus = _activeNumberButtons.Count == 0 && numbers.Count > 0;

        // 기존 숫자 버튼 클리어
        ClearNumberButtons();

        foreach (int number in numbers)
        {
            // 버튼 풀에서 숫자 버튼 가져오기
            var numberButton = _numberButtonPool.Get();

            // 숫자 버튼 초기화
            numberButton.Init(number);

            // 숫자 버튼 클릭 이벤트 구독
            numberButton.OnNumberButtonClicked += HandleOnNumberButtonClicked;
        }

        // 포커스 업데이트가 필요할 경우 업데이트
        if (shouldUpdateFocus)
        {
            UpdateFocus();
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

    public void UpdateFocus()
    {
        // 개수 가져오기
        int count = _activeNumberButtons.Count;

        // 개수가 0 이하일 경우 패스
        if (count <= 0) return;

        // 가운데 버튼 계산
        int middleIndex = count / 2;

        // 가운데 버튼 선택
        _activeNumberButtons[middleIndex].Select();
    }
    #endregion

    #region 이벤트 핸들러
    private void HandleOnNumberButtonClicked(int number)
    {
        // 숫자 버튼 클릭 이벤트 전달
        OnNumberButtonClicked?.Invoke(number);
    }
    #endregion
}