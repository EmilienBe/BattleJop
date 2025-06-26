using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleJop.Core.ModelActionResult;

public class ModelActionResult<TFaultType> : IActionResult<TFaultType> where TFaultType : Enum
{
    public TFaultType FaultType { get; }

    public bool IsSuccess { get; }

    public string Message { get; }

    public ModelActionResult(bool success, TFaultType faultType = default!, string message = default!)
    {
        IsSuccess = success;
        FaultType = faultType;
        Message = message;
    }

    public static ModelActionResult<TFaultType> Ok => new(true);
    public static ModelActionResult<TFaultType> Fail(TFaultType faultType) => new(false, faultType);
    public static ModelActionResult<TFaultType> Fail(TFaultType faultType, string message) => new(false, faultType, message);
}

public class ModelActionResult<TFaultType, T> : ModelActionResult<TFaultType> where TFaultType : Enum
{
    T Result { get; }

    public ModelActionResult(bool success, T result, TFaultType faultType = default!, string message = null!) : base(success, faultType, message)
    {
        Result = result;
    }

    public static new ModelActionResult<TFaultType, T> Ok(T result) => new(true, result);

}

public interface IActionResult<TFaultType> where TFaultType : Enum
{
    TFaultType FaultType { get; }

    bool IsSuccess { get; }

    string Message { get; }
}

public interface IActionResult<TFaultType, T> : IActionResult<TFaultType> where TFaultType : Enum
{
    T Result { get; }
}

