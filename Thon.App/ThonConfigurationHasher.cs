namespace Thon.App;

public class ThonConfigurationHasher
{
    public string Salt1 { get; }

    public string Salt2 { get; }

    public ThonConfigurationHasher(string salt1, string salt2)
    {
        Salt1 = salt1;
        Salt2 = salt2;
    }
}
