using System.Text.Json.Serialization;

namespace BestPracticesJWT.SharedCommons.Dtos;

public class ResponseDto<T> where T : class
{
    public T Data { get; private set; }
    public int StatusCode { get; private set; }
    public ErrorDto Errors { get;  set; }

    [JsonIgnore]
    public bool IsSuccessful { get; set; }

    public static ResponseDto<T> Success(T data, int statusCode) 
        => new ResponseDto<T> { Data = data, StatusCode = statusCode, IsSuccessful = true};
    public static ResponseDto<T> Success(int statusCode) 
        => new ResponseDto<T> { Data=default, StatusCode = statusCode, IsSuccessful = true};
    public static ResponseDto<T> Fail(ErrorDto errorDto, int statusCode)
        => new ResponseDto<T> { Errors = errorDto, StatusCode = statusCode, IsSuccessful = false};
    public static ResponseDto<T> Fail(string errorMessage, int statusCode, bool isShow)
    {
        var errorDto = new ErrorDto(errorMessage, isShow);
        return new ResponseDto<T> { Errors = errorDto, StatusCode = statusCode, IsSuccessful = false };
    }
}

