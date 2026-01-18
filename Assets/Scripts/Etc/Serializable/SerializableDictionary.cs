using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 직렬화 가능한 딕셔너리 클래스
/// </summary>
[Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<SerializableKeyValuePair<TKey, TValue>> items = new();

    /// <summary>
    /// 직렬화 전 호출되는 함수
    /// </summary>
    public void OnBeforeSerialize()
    {
        // 아무것도 하지 않음
    }

    /// <summary>
    /// 역직렬화 후 호출되는 함수
    /// </summary>
    public void OnAfterDeserialize()
    {
        // 딕셔너리 초기화
        Clear();

        // 리스트의 키와 값을 딕셔너리에 추가
        foreach (var kvp in items)
        {
            this[kvp.Key] = kvp.Value;
        }
    }
}
