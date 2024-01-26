using System.Reflection;

namespace WebApplication1.config;

public interface IConfig
{
    string StorageConnectionString { get; }
    string FullImageContainerName { get; }
}

public class Config : IConfig
{
    public string StorageConnectionString { get; private set; }
    public string FullImageContainerName { get; private set; } = String.Empty;
    private IConfiguration Configuration { get; set; }

    public Config(IConfiguration configuration)
    {
        this.Configuration = configuration;
        this.InitStorageData();
    }


    private void InitStorageData()
    {
        this.StorageConnectionString =
            this.Configuration.GetSection("StorageConnectionString").Value ?? String.Empty;

        if (this.StorageConnectionString == String.Empty)
        {
            throw new Exception("StorageConnectionString is undefined. Check config file");
        }
    }
}