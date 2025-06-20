namespace Microservice_Name_Token.Domain.Errors;
using ErrorOr;

public static class ErrorsMicroservice_Name_Token
{
    public static Error IsEmpty => Error.Validation("Microservice_Name_Token.Validation", "This Microservice_Name_Token does not ...");
}