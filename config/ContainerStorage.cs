using Azure.Storage.Blobs;

namespace WebApplication1.config;


public interface IStartupTask
{
    public Task Execute();
}

public interface IAzureContainerStorageConnector: IStartupTask
{
    BlobContainerClient? ContainerClient { get; }
    Task Execute();
    Task GetCloudContainer(string containerName);
}

public class AzureContainerStorageConnector: IAzureContainerStorageConnector
{
    public BlobContainerClient? ContainerClient { get; private set; }
    private readonly String AzureConnectionString;
    public AzureContainerStorageConnector(IConfig config)
    {
        this.AzureConnectionString = config.StorageConnectionString;
    }
    
    public async Task Execute()
    {
        //todo:: add container name to .env
        await this.GetCloudContainer("images");
    }

    public async Task GetCloudContainer(string containerName)
    {
        Console.WriteLine("Establishing connection to Cloud Container...");
        BlobServiceClient service = new BlobServiceClient(this.AzureConnectionString);
        BlobContainerClient containerClient = service.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
        this.ContainerClient = containerClient;
    }
}