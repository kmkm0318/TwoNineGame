using UnityEngine;

/// <summary>
/// 라운드 UI를 담당하는 클래스
/// </summary>
public class RoundUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private ProgressBar _scoreProgressBar;
    [SerializeField] private ProgressBar _roundTimeProgressBar;

    public void SetScoreMaxValue(int maxScore) => _scoreProgressBar.SetMaxValue(maxScore);
    public void SetScoreValue(int score) => _scoreProgressBar.SetValue(score);
    public void SetRoundTimeMaxValue(float maxTime) => _roundTimeProgressBar.SetMaxValue(maxTime);
    public void SetRoundTimeValue(float time) => _roundTimeProgressBar.SetValue(time);
}