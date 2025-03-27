namespace big_core.Api.Helpers;
using FluentResults;

public class ValidationError : IError
{
    public List<IError> Reasons => new();
    public string Message { get; }
    public Dictionary<string, object> Metadata { get; } = new();
    public static readonly string PROPERTY_NAME_METADATA_KEY = "PropertyName";
    public static readonly string MESSAGES_METADATA_KEY = "Messages";

    public ValidationError(string propertyName, IEnumerable<string> messages)
    {
        Message = $"Validation failed for property '{propertyName}'";
        Metadata[PROPERTY_NAME_METADATA_KEY] = propertyName;
        Metadata[MESSAGES_METADATA_KEY] = messages.ToArray();
    }

    static public List<ValidationError> CreateErrorsList(List<FluentValidation.Results.ValidationFailure> validationFailures)
    {
        var errors = validationFailures.GroupBy(e => e.PropertyName).Select(group => new ValidationError(group.Key, group.Select(e => e.ErrorMessage))).ToList();
        return errors;
    }
}
