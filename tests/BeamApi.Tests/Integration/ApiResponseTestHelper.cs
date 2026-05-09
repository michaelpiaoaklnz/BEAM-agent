using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentAssertions;

namespace BeamApi.Tests.Integration;

public class ApiResponse<T>
{
    public bool Succeeded { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<string>? Errors { get; set; }

    [JsonConverter(typeof(ApiResponseDataJsonConverter))]
    public ApiResponseData? Data { get; set; }
}

public class ApiResponseData
{
    private readonly Dictionary<string, object?> _values = new();

    public void Set(string key, object? value) => _values[key] = value;

    public ApiResponseDataItem this[string key] => new(_values.TryGetValue(key, out var v) ? v : null);
}

public class ApiResponseDataItem
{
    private readonly object? _value;
    public ApiResponseDataItem(object? value) { _value = value; }

    public dynamic Should() => _value switch
    {
        string s => s.Should(),
        bool b => b.Should(),
        int i => i.Should(),
        long l => l.Should(),
        double d => d.Should(),
        _ => ((object?)_value).Should()
    };
}

internal sealed class ApiResponseDataJsonConverter : JsonConverter<ApiResponseData>
{
    public override ApiResponseData? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return null;
        if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();

        var data = new ApiResponseData();
        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject) return data;
            if (reader.TokenType != JsonTokenType.PropertyName) throw new JsonException();

            var key = reader.GetString()!;
            reader.Read();
            object? value = reader.TokenType switch
            {
                JsonTokenType.String => reader.GetString(),
                JsonTokenType.Number => reader.TryGetInt32(out var i)
                    ? i
                    : reader.TryGetInt64(out var l)
                        ? (object)l
                        : reader.GetDouble(),
                JsonTokenType.True => true,
                JsonTokenType.False => false,
                JsonTokenType.Null => null,
                JsonTokenType.StartObject => JsonDocument.ParseValue(ref reader).RootElement.Clone(),
                JsonTokenType.StartArray => JsonDocument.ParseValue(ref reader).RootElement.Clone(),
                _ => null
            };
            data.Set(key, value);
        }
        throw new JsonException();
    }

    public override void Write(Utf8JsonWriter writer, ApiResponseData value, JsonSerializerOptions options)
        => throw new NotSupportedException();
}
