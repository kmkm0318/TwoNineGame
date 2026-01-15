/// <summary>
/// 상태 인터페이스
/// </summary>
public interface IState
{
    /// <summary>
    /// 상태 진입 시 호출
    /// </summary>
    void OnEnter();

    /// <summary>
    /// 상태 종료 시 호출
    /// </summary>
    void OnExit();

    /// <summary>
    /// 업데이트 시 호출
    /// </summary>
    void OnUpdate();
}