using System.Collections.Concurrent;

namespace Thon.Web.Services;

public class ApiTokenCache
{
    // Token Hash - Checking Expire At
    private readonly ConcurrentDictionary<string, DateTime> _tokens = [];

    private readonly TimeSpan _checkLifeSpan = new TimeSpan(hours: 1, minutes: 0, seconds: 0);

    public bool Get(string tokenHash)
    {
        if (_tokens.TryGetValue(tokenHash, out var expireDbCheckUtcDate))
        {
            if (expireDbCheckUtcDate > DateTime.UtcNow)
            {
                return true;
            }

            _tokens.TryRemove(tokenHash, out _);
        }

        return false;
    }

    public void Set(string tokenHash)
    {
        _tokens[tokenHash] = DateTime.UtcNow + _checkLifeSpan;
    }

    public void Remove(string tokenHash) 
        => _tokens.TryRemove(tokenHash, out _);
}
