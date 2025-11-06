using System.Text.Json;
using System.Text.Json.Serialization;
using Thon.Web.Models.Admin;

namespace Thon.Web.Converters;

public class AdminAccessLevelConverter : JsonConverter<AdminAccessLevel>
{
    public override void Write(Utf8JsonWriter writer, AdminAccessLevel value, JsonSerializerOptions options) => writer.WriteStringValue(value.ToString());

    public override AdminAccessLevel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}