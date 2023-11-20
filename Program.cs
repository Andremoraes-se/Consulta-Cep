using ApirRest.Providers;
using ApirRest.Services;
using ApirRest.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<EnderecoDatabaseSettings>(builder.Configuration.GetSection("EnderecoDatabase"));
builder.Services.AddSingleton<IHttpClient, HttpClientMicrosoft>();
builder.Services.AddSingleton<CepService>();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(builder => builder
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
);

app.MapGet("/", () => {
    var hello = "Hello Word";

    Console.WriteLine(hello);

    return hello;
});


app.MapGet("/cep/{cep}", async (string cep, CepService service)
    => TypedResults.Ok(await service.GetCep(cep)));


app.Run();
