namespace big_core.Api.Helpers;

using System.Text.Json;

public static class JsonSettings
{
    public static readonly JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
