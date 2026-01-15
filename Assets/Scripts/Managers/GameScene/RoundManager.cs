using System;
using UnityEngine;

/// <summary>
/// 라운드 관리를 담당하는 매니저 클래스
/// </summary>
public class RoundManager : MonoBehaviour
{
    [Header("Round Settings")]
    [SerializeField] private float _roundTimeMin = 2f;
    [SerializeField] private float _roundTimeMax = 9f;
    [SerializeField] private float _roundTimeDecreaseAmount = 0.29f;

    #region 라운드 변수
    public bool IsRoundActive { get; set; } = false;
    public int CurrentRound { get; private set; } = 0;
    public float CurrentRoundTime { get; private set; } = 0f;
    #endregion

    #region 이벤트
    public event Action<int> OnCurrentRoundChanged;
    public event Action<float> OnCurrentRoundTimeChanged;
    public event Action OnRoundCleared;
    public event Action OnRoundFailed;
    #endregion

    #region 초기화
    public void Init()
    {
        IsRoundActive = false;
        CurrentRound = 0;
        CurrentRoundTime = 0f;
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
        CurrentRoundTime = Mathf.Max(CurrentRoundTime - Time.deltaTime, 0f);

        // 라운드 시간 변경 이벤트 호출
        OnCurrentRoundTimeChanged?.Invoke(CurrentRoundTime);

        // 라운드 시간이 0 이하가 되면 라운드 실패 처리
        if (CurrentRoundTime <= 0f) RoundFail();
    }

    public void StartNextRound()
    {
        // 라운드 증가
        CurrentRound++;

        // 라운드 변경 이벤트 호출
        OnCurrentRoundChanged?.Invoke(CurrentRound);

        // 라운드 시간 계산
        float newRoundTime = _roundTimeMax - _roundTimeDecreaseAmount * (CurrentRound - 1);

        // 최소 라운드 시간 적용
        CurrentRoundTime = Mathf.Max(newRoundTime, _roundTimeMin);

        // 라운드 시간 변경 이벤트 호출
        OnCurrentRoundTimeChanged?.Invoke(CurrentRoundTime);

        // 라운드 활성화
        IsRoundActive = true;
    }

    public void RoundClear()
    {
        // 라운드가 활성화되어 있지 않으면 반환
        if (!IsRoundActive) return;

        // 라운드 비활성화
        IsRoundActive = false;

        // 라운드 클리어 이벤트 호출
        OnRoundCleared?.Invoke();
    }

    public void RoundFail()
    {
        // 라운드가 활성화되어 있지 않으면 반환
        if (!IsRoundActive) return;

        // 라운드 비활성화
        IsRoundActive = false;

        // 라운드 실패 이벤트 호출
        OnRoundFailed?.Invoke();
    }
    #endregion
}