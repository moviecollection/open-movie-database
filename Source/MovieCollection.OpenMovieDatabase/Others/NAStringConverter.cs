using Newtonsoft.Json;
using System;

namespace MovieCollection.OpenMovieDatabase.Converters
{
    public class NAStringConverter : JsonConverter
    {
        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader is null || reader.Value is null)
            {
                return null;
            }

            string text = reader.Value.ToString();
            return text.Equals("N/A", StringComparison.InvariantCultureIgnoreCase) ? null : text;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Not needed because this converter cannot write json");
        }
    }
}