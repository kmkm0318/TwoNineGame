using System;
using UnityEngine;

/// <summary>
/// 라운드 관리를 담당하는 매니저 클래스
/// </summary>
public class RoundManager : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField] private float _baseRoundTime = 5f;
    [SerializeField] private float _roundTimeDecreaseRate = 0.9f;
    [SerializeField] private float _minRoundTime = 1f;

    #region 레퍼런스
    private GameManager _gameManager;
    #endregion

    #region 라운드 변수
    public bool IsRoundActive { get; set; } = false;
    public int CurrentRound { get; private set; } = 0;
    public float CurrentRoundTime { get; private set; } = 0f;
    #endregion

    #region 이벤트
    public Action<bool> OnIsRoundActiveChanged;
    public Action<int> OnCurrentRoundChanged;
    public Action<float> OnCurrentRoundTimeChanged;
    #endregion

    #region 초기화
    public void Init(GameManager gameManager)
    {
        // 게임 매니저 할당
        _gameManager = gameManager;
    }
    #endregion

    private void Update()
    {
        HandleRoundTime();
    }

    #region 라운드
    private void HandleRoundTime()
    {
        // 라운드가 활성화되어 있지 않으면 반환
        if (!IsRoundActive) return;

        // 라운드 시간 감소
        CurrentRoundTime -= Time.deltaTime;

        // 라운드 시간 변경 이벤트 호출
        OnCurrentRoundTimeChanged?.Invoke(CurrentRoundTime);

        // 라운드 시간이 0 이하가 되면 라운드 종료 처리
        if (CurrentRoundTime <= 0f) EndCurrentRound();
    }

    public void StartNextRound()
    {
        // 라운드 증가
        CurrentRound++;

        // 라운드 변경 이벤트 호출
        OnCurrentRoundChanged?.Invoke(CurrentRound);

        // 라운드 시간 계산
        float newRoundTime = _baseRoundTime * Mathf.Pow(_roundTimeDecreaseRate, CurrentRound - 1);

        // 최소 라운드 시간 적용
        CurrentRoundTime = Mathf.Max(newRoundTime, _minRoundTime);

        // 라운드 시간 변경 이벤트 호출
        OnCurrentRoundTimeChanged?.Invoke(CurrentRoundTime);

        // 라운드 활성화
        IsRoundActive = true;

        // 라운드 시작 이벤트 호출
        OnIsRoundActiveChanged?.Invoke(IsRoundActive);
    }

    public void EndCurrentRound()
    {
        // 라운드 비활성화
        IsRoundActive = false;

        // 라운드 종료 이벤트 호출
        OnIsRoundActiveChanged?.Invoke(IsRoundActive);
    }
    #endregion
}