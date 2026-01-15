using System;
using System.Collections.Generic;
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
            (button) => { button.gameObject.SetActive(true); _activeNumberButtons.Add(button); },
            (button) => { button.gameObject.SetActive(false); _activeNumberButtons.Remove(button); },
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
    }

    public void ClearNumberButtons()
    {
        // 활성화된 모든 숫자 버튼 반환
        for (int i = _activeNumberButtons.Count - 1; i >= 0; i--)
        {
            // 버튼 가져오기
            var numberButton = _activeNumberButtons[i];

            // 이벤트 구독 해제
            numberButton.OnNumberButtonClicked -= HandleOnNumberButtonClicked;

            // 버튼 풀에 반환
            _numberButtonPool.Release(numberButton);
        }
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