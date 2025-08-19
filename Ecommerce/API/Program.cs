var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Funcionalidade - Requisições
// -  URL/Caminho/Endereço
// -  Um método HTTP

app.MapGet("/", () => "Minha próxima API em C# ");

app.MapGet("/endereco", () => "Outro texto ");

app.MapPost("/endereco", () => "Funcionalidade do post ");

app.Run();
