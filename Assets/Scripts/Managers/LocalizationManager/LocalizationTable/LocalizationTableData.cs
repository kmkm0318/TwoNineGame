using UnityEngine;

/// <summary>
/// 로컬라이제이션 테이블을 담는 스크립터블 오브젝트
/// </summary>
[CreateAssetMenu(fileName = "LocalizationTableData", menuName = "SO/Localization/LocalizationTableData", order = 0)]
public class LocalizationTableData : ScriptableObject
{
    [Header("Localization Table")]
    [SerializeField] private SerializableDictionary<LanguageType, LocalizedTextTableData> _localizationTable = new();
    public SerializableDictionary<LanguageType, LocalizedTextTableData> LocalizationTable => _localizationTable;
}