/// <summary>
/// 사용자 입력을 관리하는 싱글톤 클래스
/// </summary>
public class InputManager : Singleton<InputManager>
{
    public DefaultInput DefaultInput { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        // InputActions 초기화
        InitInputActions();
    }

    #region 초기화
    private void InitInputActions()
    {
        // 객체 생성
        DefaultInput = new();

        // InputActions 활성화
        DefaultInput.Enable();
    }
    #endregion
}