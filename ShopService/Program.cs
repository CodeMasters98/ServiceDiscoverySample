var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServiceDiscovery();

//builder.Services.AddHttpClient("shipping", static client =>
//{
//    client.BaseAddress = new("https://shipping");
//})
//.UseServiceDiscovery();

builder.Services.ConfigureHttpClientDefaults(static http =>
{
    // Turn on service discovery by default
    http.AddServiceDiscovery();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/Order", () =>
{
    return $"Your order has been shipped at {DateTimeOffset.UtcNow}";
})
.WithName("GetOrder")
.WithOpenApi();


app.Run();
