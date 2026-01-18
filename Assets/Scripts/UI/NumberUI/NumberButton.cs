using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 숫자 버튼 UI를 담당하는 클래스
/// </summary>
public class NumberButton : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Button _button;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _numberText;

    #region 데이터
    public int Number { get; private set; }
    #endregion

    #region 변수
    private Tween _colorTween;
    #endregion

    #region 이벤트
    public event Action<NumberButton> OnNumberButtonClicked;
    #endregion

    private void Awake()
    {
        // 버튼 클릭 이벤트 등록
        _button.onClick.AddListener(() => OnNumberButtonClicked?.Invoke(this));
    }

    #region UI 함수
    public void SetColor(Color color, float duration = 0.5f, Ease ease = Ease.Linear, Action onComplete = null)
    {
        // 진행 중인 색상 트윈이 있으면 종료
        _colorTween?.Kill();

        if (duration <= 0)
        {
            // 즉시 색상 변경
            _background.color = color;

            // 콜백 호출
            onComplete?.Invoke();
        }
        else
        {
            // 색상 변경 애니메이션 실행
            _colorTween = _background.DOColor(color, duration).SetEase(ease).OnComplete(() => onComplete?.Invoke());
        }
    }

    public void SetNumber(int number)
    {
        // 숫자 설정
        Number = number;

        // 숫자 텍스트 업데이트
        _numberText.text = Number.ToString();
    }

    public void Select()
    {
        _button.Select();
    }
    #endregion
}