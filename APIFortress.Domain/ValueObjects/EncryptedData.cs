namespace ApiFortress.Domain.ValueObjects
{
    public class EncryptedData
    {
        public byte[] CipherText { get; private set; }
        public byte[] InitializationVector { get; private set; }

        public EncryptedData(byte[] cipherText, byte[] initializationVector)
        {
            CipherText = cipherText ?? throw new ArgumentNullException(nameof(cipherText));
            InitializationVector = initializationVector ?? throw new ArgumentNullException(nameof(initializationVector));
        }
    }
}