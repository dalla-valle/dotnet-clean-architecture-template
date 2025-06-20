namespace Microservice_Name_Token.Api.Mapper;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a validation error with a pointer and a list of details.
/// </summary>
public class ValidationError
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationError"/> class.
    /// </summary>
    /// <param name="pointer">The pointer to the location of the error.</param>
    /// <param name="details">The details of the validation error.</param>
    public ValidationError(string? pointer, IEnumerable<string>? details)
    {
        this.Pointer = pointer;
        this.Details = details?.ToList();
    }

    /// <summary>
    /// Gets the pointer to the location of the error.
    /// </summary>
    public string? Pointer { get; }

    /// <summary>
    /// Gets the details of the validation error.
    /// </summary>
    public List<string>? Details { get; }
}