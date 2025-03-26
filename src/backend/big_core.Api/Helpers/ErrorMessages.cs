namespace big_core.Api.Helpers;

public static class ErrorMessages
{
    public const string API_REQUEST_FAILED_ERROR = "API request failed";
    public const string UNEXPECTED_API_RESPONSE_ERROR = "Unable to deserialize response";
    public const string FUTURE_DATE_ERROR = "Start date cannot be in the future.";
    public const string BIGGER_START_DATE_ERROR = "Start date cannot be greater than the end date.";
    public const string DATE_OUT_OF_RANGE = "Start date is out of the allowed range.";
    public const string EMPTY_STRING_FILTER_ERROR = "Filters cannot contain empty strings.";
    public const string INVALID_PAGE_ACTIVE = "Page active must be zero or a positive number.";
    public const string INVALID_ROWS_ERROR = "Rows must be greater than zero.";
}
