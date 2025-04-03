namespace SharedLib
{
    public abstract class BaseResult
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class Result : BaseResult
    {
        public Result(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public static Result Success(string message) => new Result(message, true);
        public static Result Failure(string message) => new Result(message, false);

    }

    public class Result<T> : BaseResult
    {
        public T Data { get; set; }

        public Result(string message, bool isSuccess, T value)
        {
            Message = message;
            IsSuccess = isSuccess;
            Data = value;
        }

        public static Result<T> Success(string messsage, T value) => new Result<T>(messsage, true, value);
        public static Result<T> Failure(string messsage, T value) => new Result<T>(messsage, false, value);

    }
}
