namespace MovieCollection.OpenMovieDatabase
{
    public class UrlParameter : object
    {
        public UrlParameter(string key, string value)
            : base()
        {
            Key = key;
            Value = value;
        }

        public UrlParameter(string key, object value)
            : base()
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new System.ArgumentException("Key cannot be null or whitespace.", nameof(key));
            }

            if (value is null)
            {
                throw new System.ArgumentNullException(nameof(value));
            }

            Key = key;
            Value = value.ToString();
        }

        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return $"{Key}={Value}";
        }
    }
}
