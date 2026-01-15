using TMPro;
using UnityEngine;

/// <summary>
/// 라운드 UI를 담당하는 클래스
/// </summary>
public class RoundUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text _roundText;
    [SerializeField] private TMP_Text _roundTimeText;

    public void UpdateRoundText(int round) => _roundText.text = $"{round}";
    public void UpdateRoundTimeText(float time) => _roundTimeText.text = $"{time:F1}s";
}