using Thon.App.Exceptions;
using Thon.App.Helpers;
using Thon.App.Models;
using Thon.Core.Models;
using Thon.Storage;

namespace Thon.App.Services;

public class ApiTokenService(
    StorageService storage,
    Hasher hasher)
{
    public async Task<IReadOnlyList<ApiToken>> Get(CancellationToken cancellationToken = default)
    {
        var tokens = await storage.ApiTokens.Get(cancellationToken);
        return tokens;
    }

    public async Task<ApiToken?> Get(
        Guid id, 
        CancellationToken cancellationToken = default)
    {
        var token = await storage.ApiTokens
            .Get(
                id: id, 
                cancellationToken: cancellationToken);

        return token;
    }

    public async Task<ApiToken?> Get(
        string tokenHash, 
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNullOrWhiteSpace(tokenHash);

        var token = await storage.ApiTokens
            .Get(
                tokenHash: tokenHash, 
                cancellationToken: cancellationToken);

        return token;
    }

    public async Task<ApiTokenCreateResult> Create(CancellationToken cancellationToken = default)
    {
        var tokenRawPart1 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        var tokenRawPart2 = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        var tokenRaw = $"{tokenRawPart1}_{tokenRawPart2}";

        var tokenHash = hasher.Sha256Salted(tokenRaw);
        var token = new ApiToken(tokenHash);

        await storage.ApiTokens
            .Insert(
                model: token, 
                cancellationToken: cancellationToken);

        return new ApiTokenCreateResult(
            tokenRaw: tokenRaw,
            token: token);
    }

    public async Task Delete(
        ApiToken token,
        CancellationToken cancellationToken = default)
    {
        ThonArgumentException.ThrowIfNull(token);

        await storage.ApiTokens
            .Delete(
                model: token, 
                cancellationToken: cancellationToken);
    }
}
