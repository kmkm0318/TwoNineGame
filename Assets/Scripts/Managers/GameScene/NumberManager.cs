using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 숫자를 관리하는 매니저 클래스
/// </summary>
public class NumberManager : MonoBehaviour
{
    #region 상수
    private const int MIN_TARGET_MULTIPLE = 2;
    private const int MAX_TARGET_MULTIPLE = 9;
    private const int NUMBER_COUNT = 9;
    private const int CORRECT_NUMBER_COUNT = 2;
    private const int MIN_NUMBER_VALUE = 2;
    private const int MAX_NUMBER_VALUE = 99;
    #endregion

    #region 레퍼런스
    private GameManager _gameManager;
    #endregion

    #region 숫자 관련 변수
    public int CurrentTargetMultiple { get; private set; } = MIN_TARGET_MULTIPLE;
    private readonly Dictionary<int, List<int>> _correctNumbers = new();
    private readonly Dictionary<int, List<int>> _wrongNumbers = new();
    public List<int> Numbers { get; private set; } = new();
    #endregion

    #region 이벤트
    public event Action<int> OnCurrentTargetMultipleChanged;
    public event Action<List<int>> OnNumbersChanged;
    #endregion

    #region 초기화
    public void Init(GameManager gameManager)
    {
        // 레퍼런스 설정
        _gameManager = gameManager;

        // 올바른 숫자 및 잘못된 숫자 리스트 초기화
        InitCorrectWrongNumbers();
    }

    private void InitCorrectWrongNumbers()
    {
        // 각 배수에 대해 실행
        for (int multiple = MIN_TARGET_MULTIPLE; multiple <= MAX_TARGET_MULTIPLE; multiple++)
        {
            // 리스트 초기화
            _correctNumbers[multiple] = new List<int>();
            _wrongNumbers[multiple] = new List<int>();

            // 숫자 범위 내에서 배수 판별
            for (int number = MIN_NUMBER_VALUE; number <= MAX_NUMBER_VALUE; number++)
            {
                if (number % multiple == 0)
                {
                    // 배수인 경우 올바른 숫자 리스트에 추가
                    _correctNumbers[multiple].Add(number);
                }
                else
                {
                    // 배수가 아닌 경우 잘못된 숫자 리스트에 추가
                    _wrongNumbers[multiple].Add(number);
                }
            }
        }
    }
    #endregion

    #region 숫자 관리
    public void IncreaseTargetMultiple()
    {
        // 다음 목표 배수 계산
        var newTargetMultiple = CurrentTargetMultiple + 1;

        // 범위 밖의 값이면 최소값으로 초기화
        if (newTargetMultiple < MIN_TARGET_MULTIPLE || newTargetMultiple > MAX_TARGET_MULTIPLE)
        {
            newTargetMultiple = MIN_TARGET_MULTIPLE;
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
        if (!_correctNumbers.TryGetValue(CurrentTargetMultiple, out var correctList))
        {
            $"올바른 숫자 리스트를 찾을 수 없습니다. 배수: {CurrentTargetMultiple}".LogError();
            return;
        }

        // 올바른 숫자들 중에서 무작위로 선택
        var correctNumber = correctList.GetRandomElements(CORRECT_NUMBER_COUNT);

        // 올바른 숫자들 추가
        Numbers.AddRange(correctNumber);

        // 잘못된 숫자 리스트 가져오기
        if (!_wrongNumbers.TryGetValue(CurrentTargetMultiple, out var wrongList))
        {
            $"잘못된 숫자 리스트를 찾을 수 없습니다. 배수: {CurrentTargetMultiple}".LogError();
            return;
        }

        // 필요한 잘못된 숫자 개수 계산
        int wrongNumberCount = NUMBER_COUNT - CORRECT_NUMBER_COUNT;

        // 잘못된 숫자들 중에서 무작위로 선택
        var wrongNumbers = wrongList.GetRandomElements(wrongNumberCount);

        // 잘못된 숫자들 추가
        Numbers.AddRange(wrongNumbers);

        // 생성된 숫자 개수 검증
        if (Numbers.Count != NUMBER_COUNT)
        {
            "생성된 숫자 개수가 올바르지 않습니다.".LogError();
            return;
        }

        // 숫자 리스트 섞기
        Numbers.Shuffle();

        // 숫자 변경 이벤트 호출
        OnNumbersChanged?.Invoke(Numbers);
    }
    #endregion

    #region 숫자 테스트
    public bool IsNumberCorrect(int number)
    {
        // 숫자가 현재 목표 배수의 배수인지 판별
        return number % CurrentTargetMultiple == 0;
    }
    #endregion
}