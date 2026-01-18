using System;

/// <summary>
/// 직렬화 가능한 키, 값 쌍 struct
/// </summary>
[Serializable]
public struct SerializableKeyValuePair<TKey, TValue>
{
    public TKey Key;
    public TValue Value;

    public SerializableKeyValuePair(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}