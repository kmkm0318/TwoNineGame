using UnityEngine;

/// <summary>
/// 세이프 에어리어 처리 클래스
/// 화면의 안전 영역을 고려하여 UI 요소의 위치를 조정합니다.
/// </summary>
[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    #region 컴포넌트
    private RectTransform _rectTransform;
    #endregion

    private void Awake()
    {
        // 컴포넌트 가져오기
        _rectTransform = GetComponent<RectTransform>();

        // 세이프 에리어 업데이트
        UpdateSafeArea();
    }

#if UNITY_EDITOR
    private void Update()
    {
        // 에디터 모드에서는 매 프레임마다 업데이트
        UpdateSafeArea();
    }
#endif

    private void UpdateSafeArea()
    {
        // 세이프 에리어 가져오기
        var safeArea = Screen.safeArea;

        // 앵커 계산
        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;

        // 화면 크기로 정규화
        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        // 앵커 설정
        _rectTransform.anchorMin = anchorMin;
        _rectTransform.anchorMax = anchorMax;
    }
}