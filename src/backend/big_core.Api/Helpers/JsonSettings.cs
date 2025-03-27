namespace big_core.Api.Helpers;

using System.Text.Json;
using System.Text.Json.Serialization;

public static class JsonSettings
{
    public static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals,
        WriteIndented = true
    };
}
