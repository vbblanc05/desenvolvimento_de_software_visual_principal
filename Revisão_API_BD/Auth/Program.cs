using Auth.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapGet("/", () => "Revisão da API com Banco de Dados + BCrypt!");

//POST: /api/usuario/cadastrar
app.MapPost("/api/usuario/cadastrar", (
    [FromBody] Usuario usuario,
    [FromServices] AppDataContext ctx) =>
{
    //Validar os campos de e-mail e senha
    if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
    {
        return Results.BadRequest("Os campos de e-mail ou senha precisam estar preenchidos!");
    }

    //Validar o e-mail do usuário
    bool resultado = ctx.Usuarios.Any(x => x.Email == usuario.Email);
    if (resultado)
    {
        return Results.Conflict("Este usuário já está em uso!");
    }

    //Gerar o hash da senha
    string hashSenha = BCrypt.Net.BCrypt.HashPassword(usuario.Senha);
    usuario.Senha = hashSenha;

    ctx.Usuarios.Add(usuario);
    ctx.SaveChanges();
    return Results.Created("", usuario);
});

//GET: /api/usuario/listar
app.MapGet("/api/usuario/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Usuarios.Any())
    {
        return Results.Ok(ctx.Usuarios.ToList());
    }
    return Results.NotFound("Não existe nenhum registro de usuário!");
});

//POST: /api/usuario/login
app.MapPost("/api/usuario/login", (
    [FromBody] Usuario usuario,
    [FromServices] AppDataContext ctx) =>
{
    //Validar os campos de e-mail e senha
    if (string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Senha))
    {
        return Results.BadRequest("Os campos de e-mail ou senha precisam estar preenchidos!");
    }

    //Validar o usuário
    Usuario? resultado = ctx.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);
    if (resultado is null)
    {
        return Results.Unauthorized();
    }

    //Validar a senha
    bool validarSenha = BCrypt.Net.BCrypt.Verify(usuario.Senha, resultado!.Senha);
    if (!validarSenha)
    {
        return Results.Unauthorized();
    }

    //Login e senha válidos!
    return Results.Ok("Login efetuado com sucesso!");

});


app.Run();
