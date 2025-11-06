using System.Security.Cryptography;
using System.Text;

namespace Thon.App.Helpers;

public class Hasher(ThonConfigurationHasher configuration)
{
    /// <summary>
    /// Валидация хэшированной строки с солью.
    /// </summary>
    /// <param name="input">Входная строка.</param>
    /// <param name="saltedHash">"Солёный" хэш строки.</param>
    /// <returns>Подходит ли строка к хэшу. True, если подходит, иначе fasle.</returns>
    public bool VerifySha256Salted(string input, string saltedHash) => Sha256Salted(input) == saltedHash;

    /// <summary>
    /// Получить SHA-256 хэш строки с солью.
    /// К входной строке будет добавлены соли для усложнения подбора хэша.
    /// </summary>
    /// <param name="input">Входная строка.</param>
    /// <returns>SHA-256 хэшированная строка с солями.</returns>
    public string Sha256Salted(string input) => Sha256(configuration.Salt1 + input + configuration.Salt2);

    /// <summary>
    /// Получить SHA-256 хэш строки.
    /// </summary>
    /// <param name="input">Входная строка.</param>
    /// <returns>SHA-256 хэшированная строка.</returns
    public string Sha256(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).Replace("-", "").ToLower();
    }
}
