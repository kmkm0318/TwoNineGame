using TMPro;
using UnityEngine;

/// <summary>
/// 라운드 UI를 담당하는 클래스
/// </summary>
public class RoundUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private ProgressBar _roundProgressBar;
    [SerializeField] private ProgressBar _roundTimeProgressBar;

    public void UpdateMaxRound(int maxRound) => _roundProgressBar.SetMaxValue(maxRound);
    public void UpdateRound(int round) => _roundProgressBar.SetValue(round);
    public void UpdateMaxRoundTime(float maxTime) => _roundTimeProgressBar.SetMaxValue(maxTime);
    public void UpdateRoundTime(float time) => _roundTimeProgressBar.SetValue(time);
}