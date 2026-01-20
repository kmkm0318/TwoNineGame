using UnityEngine;

/// <summary>
/// 로컬라이즈된 텍스트 테이블을 담는 스크립터블 오브젝트
/// </summary>
[CreateAssetMenu(fileName = "LocalizedTextTableData", menuName = "SO/Localization/LocalizedTextTableData", order = 0)]
public class LocalizedTextTableData : ScriptableObject
{
    [Header("Localized Text Table")]
    [SerializeField] private SerializableDictionary<string, string> _localizedTextTable = new();
    public SerializableDictionary<string, string> LocalizedTextTable => _localizedTextTable;
}