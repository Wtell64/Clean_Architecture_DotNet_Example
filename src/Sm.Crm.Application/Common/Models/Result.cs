namespace Sm.Crm.Application.Common.Models;

public class Result<T>
{
    public bool Succeeded { get; init; }
    public string[] Errors { get; init; }
    public T? Data { get; init; }
    public string Message { get; init; }

    public Result(bool succeeded, T? data, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Data = data;
        Errors = errors.ToArray();
    }

    public static Result<T> Success(T data)
    {
        return new(true, data, Array.Empty<string>());
    }

    public static Result<T> Success(string message)
    {
        return new(true, default, Array.Empty<string>()) { Message = message };
    }

    public static Result<T> Success(T data, string message)
    {
        return new(true, data, Array.Empty<string>()) { Message = message };
    }

    public static Result<T> Failure(IEnumerable<string> errors, string message)
    {
        return new(false, default, errors) { Message = message };
    }

    public static Result<T> Failure(string message)
    {
        return new(false, default, Array.Empty<string>()) { Message = message };
    }

    public static Result<T> NotFound()
    {
        return new(false, default, new[] { "NotFound" });
    }
}