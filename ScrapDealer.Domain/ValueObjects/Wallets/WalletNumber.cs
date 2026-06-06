using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Shared.Abstractions.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace ScrapDealer.Domain.ValueObjects.Wallets;

public class WalletNumber : ValueObject
{
    public string Value { get; }

    private WalletNumber() { }
    private WalletNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessException("شماره کیف پول نمی‌تواند خالی باشد.");

        if (value.Length != 16)
            throw new BusinessException("شماره کیف پول باید دقیقاً 16 کاراکتر باشد.");

        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^[A-Z0-9]+$"))
            throw new BusinessException("فرمت شماره کیف پول نامعتبر است.");

        Value = value;
    }

    public static WalletNumber Create(string value)
    {
        return new WalletNumber(value);
    }

    public static WalletNumber Generate()
    {
        var randomBytes = new byte[12];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        var walletNumber = Convert.ToBase64String(randomBytes)
            .Replace('+', 'X')
            .Replace('/', 'Y')
            .Replace('=', 'Z')
            .Substring(0, 16);

        return new WalletNumber(walletNumber);
    }

    public static WalletNumber GenerateFromNationalCode(string nationalCode)
    {
        if (string.IsNullOrWhiteSpace(nationalCode) || nationalCode.Length != 10)
            throw new BusinessException("کد ملی معتبر نمی‌باشد.");

        var prefix = nationalCode.Substring(6, 4);

        var randomBytes = new byte[8]; // 8 bytes = 64 bits
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        var randomNumber = BitConverter.ToUInt64(randomBytes, 0) % 1_000_000_000_000; // Mod 10^12
        var suffix = randomNumber.ToString("D12"); // Pad with leading zeros to 12 digits

        var walletNumber = $"{prefix}{suffix}";

        return new WalletNumber(walletNumber);
    }

    public static WalletNumber GenerateWithChecksum()
    {
        var randomBytes = new byte[10];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        var basePart = Convert.ToHexString(randomBytes).Substring(0, 14);
        var checksum = CalculateChecksum(basePart);
        var walletNumber = $"{basePart}{checksum}";

        return new WalletNumber(walletNumber);
    }

    private static string CalculateChecksum(string input)
    {
        var hash = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(hash).Substring(0, 2);
    }

    public static bool IsValidFormat(string walletNumber)
    {
        try
        {
            var _ = new WalletNumber(walletNumber);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj is WalletNumber other)
            return Value == other.Value;

        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static implicit operator string(WalletNumber walletNumber)
        => walletNumber.Value;

    public static implicit operator WalletNumber(string value)
        => Create(value);
}