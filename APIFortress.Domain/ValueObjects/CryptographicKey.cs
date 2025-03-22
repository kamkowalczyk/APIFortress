namespace ApiFortress.Domain.ValueObjects
{
    public class CryptographicKey
    {
        public string KeyValue { get; private set; }

        public CryptographicKey(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                throw new ArgumentException("Key value cannot be null or empty.", nameof(keyValue));
            }
            KeyValue = keyValue;
        }

        public override bool Equals(object obj)
        {
            if (obj is CryptographicKey otherKey)
                return KeyValue == otherKey.KeyValue;
            return false;
        }

        public override int GetHashCode() => KeyValue.GetHashCode();
    }
}