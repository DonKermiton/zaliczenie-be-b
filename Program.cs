using WebApplication1.config;
using WebApplication1.services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policy =>
    {
        policy.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowAnyOrigin();
    });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfig, Config>();
builder.Services.AddSingleton<IAzureContainerStorageConnector, AzureContainerStorageConnector>();
builder.Services.AddSingleton<IAzureContainerStorageFacade, AzureContainerStorageFacade>();

var app = builder.Build();

await (app.Services.GetService<IAzureContainerStorageConnector>()!).Execute();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("FrontEndClient");
app.UseAuthorization();

app.MapControllers();

app.Run();