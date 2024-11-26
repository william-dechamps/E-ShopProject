using EShopProject;

var builder = WebApplication.CreateBuilder(args);
builder.Services.InitServices(builder.Configuration);

var app = builder.Build();

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllOrigins");
app.MapControllers();

app.Run();

public partial class Program { }