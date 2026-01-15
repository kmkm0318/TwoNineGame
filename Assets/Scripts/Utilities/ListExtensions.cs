using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 리스트 확장 메서드 클래스
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// 리스트에서 랜덤 요소 하나를 반환
    /// </summary>
    public static T GetRandomElement<T>(this List<T> list)
    {
        // 유효성 검사
        if (list == null || list.Count <= 0) return default;

        // 랜덤 인덱스 선택
        int idx = Random.Range(0, list.Count);

        // 선택된 요소 반환
        return list[idx];
    }

    /// <summary>
    /// 리스트에서 중복 없이 랜덤 요소 count개 반환
    /// </summary>
    public static List<T> GetRandomElements<T>(this List<T> list, int count)
    {
        // 결과 리스트
        List<T> res = new();

        // 유효성 검사
        if (list == null || list.Count <= 0 || count <= 0) return res;

        // 임시 리스트 생성 (원본 리스트 복사)
        List<T> tempList = new(list);

        // 중복 없이 랜덤 요소 선택
        for (int i = 0; i < count && tempList.Count > 0; i++)
        {
            // 랜덤 인덱스 선택
            int idx = Random.Range(0, tempList.Count);

            // 선택된 요소 결과 리스트에 추가
            res.Add(tempList[idx]);

            // 선택된 요소 임시 리스트에서 제거
            tempList.RemoveAt(idx);
        }

        // 결과 반환
        return res;
    }

    /// <summary>
    /// 리스트 섞기
    /// </summary>
    public static void Shuffle<T>(this List<T> list)
    {
        // 유효성 검사
        if (list == null || list.Count <= 1) return;

        // 리스트 요소 섞기
        for (int i = 0; i < list.Count; i++)
        {
            // 랜덤 인덱스 선택
            int idx = Random.Range(0, list.Count);

            // 요소 교환
            (list[idx], list[i]) = (list[i], list[idx]);
        }
    }
}