using Thon.App;

namespace Thon.Web.Helpers;

public class ThonConfigurationGenerator(IConfiguration configuration)
{
    public ThonConfiguration Get()
    {
        var databaseConnectionString = 
            Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING")
            ?? configuration.GetValue<string>("Configuration:Database:ConnectionString")
            ?? throw new InvalidOperationException("Database connection string not defined!");

        var initialAdministratorLogin = 
            Environment.GetEnvironmentVariable("INIT_ADMIN_LOGIN")
            ?? configuration.GetValue<string>("Configuration:Initialization:AdminLogin")
            ?? throw new InvalidOperationException("Initialization Admin Login not defined!");

        var initialAdministratorPassword =
            Environment.GetEnvironmentVariable("INIT_ADMIN_PASSWORD")
            ?? configuration.GetValue<string>("Configuration:Initialization:AdminPassword")
            ?? throw new InvalidOperationException("Initialization Admin Password not defined!");

        var hasherSalt1 =
           Environment.GetEnvironmentVariable("HASH_SALT_1")
           ?? configuration.GetValue<string>("Configuration:Hasher:Salt1")
           ?? throw new InvalidOperationException("Hasher Salt 1 not defined!");

        var hasherSalt2 =
           Environment.GetEnvironmentVariable("HASH_SALT_2")
           ?? configuration.GetValue<string>("Configuration:Hasher:Salt2")
           ?? throw new InvalidOperationException("Hasher Salt 2 not defined!");

        var databaseConfiguration = new ThonConfigurationStorage(
            connectionString: databaseConnectionString);

        var initializatinConfiguration = new ThonConfigurationInitialization(
            adminLogin: initialAdministratorLogin, 
            adminPassword: initialAdministratorPassword);

        var hashesConfiguration = new ThonConfigurationHasher(
            salt1: hasherSalt1,
            salt2: hasherSalt2);

        return new ThonConfiguration(
            hasher: hashesConfiguration,
            storage: databaseConfiguration,
            initializaiton: initializatinConfiguration);
    }
}
