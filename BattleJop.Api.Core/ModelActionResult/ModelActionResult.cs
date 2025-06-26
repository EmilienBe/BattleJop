namespace BattleJop.Api.Core.ModelActionResult;

public class ModelActionResult : IActionResult
{
    public FaultType FaultType { get; }

    public bool IsSuccess { get; }

    public string Message { get; }

    public ModelActionResult(bool success, FaultType faultType = default!, string message = default!)
    {
        IsSuccess = success;
        FaultType = faultType;
        Message = message;
    }

    public static ModelActionResult Ok() => new(true);
    public static ModelActionResult Fail(FaultType faultType) => new(false, faultType);
    public static ModelActionResult Fail(FaultType faultType, string message) => new(false, faultType, message);
}

public class ModelActionResult<T> : ModelActionResult where T : class
{
    public T Result { get; }

    public ModelActionResult(bool success, T result, FaultType faultType = default!, string message = null!) : base(success, faultType, message)
    {
        Result = result;
    }

    public ModelActionResult(bool success, FaultType faultType = default!, string message = null!) : base(success, faultType, message)
    {

    }

    public static ModelActionResult<T> Ok(T result) => new(true, result);
    public static new ModelActionResult<T> Fail(FaultType faultType) => new(false, faultType);
    public static new ModelActionResult<T> Fail(FaultType faultType, string message) => new(false, faultType, message);
}

public interface IActionResult 
{
    FaultType FaultType { get; }

    bool IsSuccess { get; }

    string Message { get; }
}
