using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 숫자를 관리하는 매니저 클래스
/// </summary>
public class NumberManager : MonoBehaviour
{
    #region 상수
    // 초기 목표 배수. 0으로 설정하여 첫 호출 시 최소값이 되도록 함
    private const int INITIAL_TARGET_MULTIPLE = 0;
    #endregion

    #region 에디터 변수
    [Header("Number Settings")]
    [SerializeField] private int _minTargetMultiple = 2;
    [SerializeField] private int _maxTargetMultiple = 9;
    [SerializeField] private int _numberCount = 9;
    [SerializeField] private int _correctNumberCount = 2;
    [SerializeField] private int _minNumberValue = 2;
    [SerializeField] private int _maxNumberValue = 99;
    #endregion

    #region 숫자 관련 변수
    public int CurrentTargetMultiple { get; private set; } = INITIAL_TARGET_MULTIPLE;
    private readonly Dictionary<int, List<int>> _correctNumberLists = new();
    private readonly Dictionary<int, List<int>> _wrongNumberLists = new();
    public List<int> Numbers { get; private set; } = new();
    #endregion

    #region 이벤트
    public event Action<int> OnCurrentTargetMultipleChanged;
    public event Action<List<int>> OnNumbersChanged;
    public event Action OnCorrectNumberSelected;
    public event Action OnWrongNumberSelected;
    #endregion

    private void Awake()
    {
        // 올바른 숫자 및 잘못된 숫자 리스트 딕셔너리 생성
        SetupNumberLists();
    }

    #region 숫자 리스트 딕셔너리 생성
    private void SetupNumberLists()
    {
        // 각 배수에 대해 실행
        for (int multiple = _minTargetMultiple; multiple <= _maxTargetMultiple; multiple++)
        {
            // 리스트 초기화
            _correctNumberLists[multiple] = new();
            _wrongNumberLists[multiple] = new();

            // 숫자 범위 내에서 배수 판별
            for (int number = _minNumberValue; number <= _maxNumberValue; number++)
            {
                if (number % multiple == 0)
                {
                    // 배수인 경우 올바른 숫자 리스트에 추가
                    _correctNumberLists[multiple].Add(number);
                }
                else
                {
                    // 배수가 아닌 경우 잘못된 숫자 리스트에 추가
                    _wrongNumberLists[multiple].Add(number);
                }
            }
        }
    }
    #endregion

    #region 초기화
    public void Init()
    {
        // 초기 목표 배수 설정
        CurrentTargetMultiple = INITIAL_TARGET_MULTIPLE;
    }
    #endregion

    #region 숫자 관리
    public void IncreaseTargetMultiple()
    {
        // 다음 목표 배수 계산
        var newTargetMultiple = CurrentTargetMultiple + 1;

        // 범위 밖의 값이면 최소값으로 초기화
        if (newTargetMultiple < _minTargetMultiple || newTargetMultiple > _maxTargetMultiple)
        {
            newTargetMultiple = _minTargetMultiple;
        }

        // 목표 배수 업데이트
        CurrentTargetMultiple = newTargetMultiple;

        // 목표 배수 변경 이벤트 호출
        OnCurrentTargetMultipleChanged?.Invoke(CurrentTargetMultiple);
    }

    public void GenerateNumbers()
    {
        // 기존 숫자 리스트 초기화
        Numbers.Clear();

        // 올바른 숫자 리스트 가져오기
        if (!_correctNumberLists.TryGetValue(CurrentTargetMultiple, out var correctList))
        {
            $"올바른 숫자 리스트를 찾을 수 없습니다. 배수: {CurrentTargetMultiple}".LogError();
            return;
        }

        // 올바른 숫자들 중에서 무작위로 선택
        var correctNumber = correctList.GetRandomElements(_correctNumberCount);

        // 올바른 숫자들 추가
        Numbers.AddRange(correctNumber);

        // 잘못된 숫자 리스트 가져오기
        if (!_wrongNumberLists.TryGetValue(CurrentTargetMultiple, out var wrongList))
        {
            $"잘못된 숫자 리스트를 찾을 수 없습니다. 배수: {CurrentTargetMultiple}".LogError();
            return;
        }

        // 필요한 잘못된 숫자 개수 계산
        int wrongNumberCount = _numberCount - _correctNumberCount;

        // 잘못된 숫자들 중에서 무작위로 선택
        var wrongNumbers = wrongList.GetRandomElements(wrongNumberCount);

        // 잘못된 숫자들 추가
        Numbers.AddRange(wrongNumbers);

        // 생성된 숫자 개수 검증
        if (Numbers.Count != _numberCount)
        {
            $"생성된 숫자 개수가 올바르지 않습니다. 현재 개수: {Numbers.Count}".LogError();
            return;
        }

        // 숫자 리스트 섞기
        Numbers.Shuffle();

        // 숫자 변경 이벤트 호출
        OnNumbersChanged?.Invoke(Numbers);
    }
    #endregion

    #region 이벤트 핸들러
    public void HandleOnNumberButtonClicked(int number)
    {
        // 숫자가 올바른지 판별
        if (number % CurrentTargetMultiple == 0)
        {
            // 올바른 숫자 선택 이벤트 호출
            OnCorrectNumberSelected?.Invoke();
        }
        else
        {
            // 잘못된 숫자 선택 이벤트 호출
            OnWrongNumberSelected?.Invoke();
        }
    }
    #endregion
}