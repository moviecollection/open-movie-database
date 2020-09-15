using System;
using Newtonsoft.Json;

namespace MovieCollection.OpenMovieDatabase.Converters
{
    /// <summary>
    /// Provides a <see cref="JsonConverter"/> to convert "N/A" values to null.
    /// </summary>
    public class NAStringConverter : JsonConverter
    {
        /// <inheritdoc/>
        public override bool CanWrite => false;

        /// <inheritdoc/>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        /// <inheritdoc/>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader is null || reader.Value is null)
            {
                return null;
            }

            string text = reader.Value.ToString();
            return text.Equals("N/A", StringComparison.InvariantCultureIgnoreCase) ? null : text;
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Not needed because this converter cannot write json");
        }
    }
}
