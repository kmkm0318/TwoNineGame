using System;
using UnityEngine;

/// <summary>
/// 라운드 관리를 담당하는 매니저 클래스
/// </summary>
public class RoundManager : MonoBehaviour
{
    #region 상수
    private const int INITIAL_TARGET_SCORE = 29;
    #endregion

    [Header("Round Settings")]
    [SerializeField] private float _roundTimeMin = 2f;
    [SerializeField] private float _roundTimeMax = 9f;
    [SerializeField] private float _roundTimeDecreaseAmount = 0.29f;

    #region 레퍼런스
    private UserDataManager _userDataManager;
    #endregion

    #region 라운드 변수
    public bool IsRoundActive { get; private set; } = false;
    public int TargetScore { get; private set; } = INITIAL_TARGET_SCORE;
    public int CurrentScore { get; private set; } = 0;
    public float MaxRoundTime { get; private set; } = 0f;
    public float CurrentRoundTime { get; private set; } = 0f;
    #endregion

    #region 이벤트
    public event Action<int> OnTargetScoreChanged;
    public event Action<int> OnCurrentScoreChanged;
    public event Action<float> OnMaxRoundTimeChanged;
    public event Action<float> OnCurrentRoundTimeChanged;
    public event Action OnRoundCleared;
    public event Action OnRoundFailed;
    #endregion

    #region 초기화
    public void Init(UserDataManager userDataManager)
    {
        // 레퍼런스 설정
        _userDataManager = userDataManager;
    }

    public void Reset()
    {
        // 목표 점수 설정
        UpdateTargetScore(_userDataManager.UserData.BestScore);

        // 현재 점수 설정
        CurrentScore = 0;

        // 이벤트 호출
        OnCurrentScoreChanged?.Invoke(CurrentScore);

        // 현재 라운드 시간 설정
        CurrentRoundTime = 0f;

        // 이벤트 호출
        OnCurrentRoundTimeChanged?.Invoke(CurrentRoundTime);
    }
    #endregion

    private void Update()
    {
        HandleRoundTime();
    }

    #region 라운드
    public void SetRoundActive(bool isActive) => IsRoundActive = isActive;

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
        // 라운드 시간 계산
        float newRoundTime = _roundTimeMax - _roundTimeDecreaseAmount * (CurrentScore - 1);

        // 최소 라운드 시간 적용
        newRoundTime = Mathf.Max(newRoundTime, _roundTimeMin);

        // 최대 라운드 시간 변경
        MaxRoundTime = newRoundTime;

        // 이벤트 호출
        OnMaxRoundTimeChanged?.Invoke(MaxRoundTime);

        // 현재 라운드 시간 설정
        CurrentRoundTime = MaxRoundTime;

        // 이벤트 호출
        OnCurrentRoundTimeChanged?.Invoke(CurrentRoundTime);
    }

    public void RoundClear()
    {
        // 라운드가 활성화되어 있지 않으면 반환
        if (!IsRoundActive) return;

        // 라운드 비활성화
        IsRoundActive = false;

        // 현재 점수 증가
        CurrentScore++;

        // 이벤트 호출
        OnCurrentScoreChanged?.Invoke(CurrentScore);

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

    #region 변수 변경
    private void UpdateTargetScore(int newTargetScore)
    {
        // 새로운 목표 점수가 기존 목표 점수보다 작거나 같으면 변경하지 않음
        if (newTargetScore <= TargetScore) return;

        // 목표 점수 변경
        TargetScore = newTargetScore;

        // 이벤트 호출
        OnTargetScoreChanged?.Invoke(TargetScore);
    }
    #endregion
}