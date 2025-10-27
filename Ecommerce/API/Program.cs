using API.Models;
using Microsoft.AspNetCore.Mvc;

// Console.Clear();
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

//Lista de produtos fakes
List<Produto> produtos = new List<Produto>
{
    new Produto { Nome = "Camiseta Básica", Quantidade = 50, Preco = 29.99 },
    new Produto { Nome = "Calça Jeans", Quantidade = 30, Preco = 99.90 },
    new Produto { Nome = "Tênis Esportivo", Quantidade = 20, Preco = 149.50 },
    new Produto { Nome = "Mochila Escolar", Quantidade = 15, Preco = 79.99 },
    new Produto { Nome = "Relógio Digital", Quantidade = 10, Preco = 199.90 },
    new Produto { Nome = "Óculos de Sol", Quantidade = 25, Preco = 59.90 },
    new Produto { Nome = "Boné Aba Curva", Quantidade = 40, Preco = 39.99 },
    new Produto { Nome = "Jaqueta de Couro", Quantidade = 5, Preco = 299.99 },
    new Produto { Nome = "Meias Esportivas", Quantidade = 100, Preco = 9.90 },
    new Produto { Nome = "Cinto de Couro", Quantidade = 35, Preco = 49.90 }
};

//Funcionalidade - Requisições
// - URL/Caminho/Endereço
// - Um método HTTP
// - Dados: rota (URL) e corpo (opcional)

//Respostas
// - Código de status HTTP
// - Corpo/Dados

// Métodos HTTP:
// GET    - Recuperar/Enviar dados da sua API/aplicação
// POST   - Enviar/Cadastrar dados para criar um novo recurso
// PUT    - Atualiza um recurso existente
// DELETE - Remove um recurso
// PATCH  - Atualiza parcialmente um recurso

//Códigos de Status HTTP
// 2xx (Sucesso)
// 200 OK: A solicitação foi bem-sucedida e o servidor retornou a resposta esperada.
// 201 Created: A solicitação foi bem-sucedida e um novo recurso foi criado como resultado (geralmente usado em POST).
// 204 No Content: A solicitação foi bem-sucedida, mas não há conteúdo para retornar (geralmente em respostas de DELETE ou PUT sem necessidade de retornar dados).
// 4xx (Erro do Cliente)
// 400 Bad Request: A solicitação é inválida ou malformada; o servidor não conseguiu entendê-la.
// 401 Unauthorized: O cliente não tem permissão para acessar o recurso, geralmente porque precisa autenticar-se.
// 404 Not Found: O recurso solicitado não foi encontrado no servidor.
// 409 Conflict: A solicitação não pôde ser processada devido a um conflito, geralmente relacionado a dados (como tentar criar um recurso com o mesmo identificador que outro já existe).

app.MapGet("/", () => "API de Produtos");

//GET: /api/produto/listar
app.MapGet("/api/produto/listar",
    ([FromServices] AppDataContext ctx) =>
{
    //Validar a lista de produtos para saber 
    //se existe algo dentro
    if (ctx.Produtos.Any())
    {
        return Results.Ok(ctx.Produtos.ToList());
    }
    return Results.NotFound("Lista vazia!");
});

//GET: /api/produto/buscar/nome_do_produto
app.MapGet("/api/produto/buscar/{nome}",
    ([FromRoute] string nome,
    [FromServices] AppDataContext ctx) =>
{
    //Expressão lambda
    Produto? resultado =
        ctx.Produtos.FirstOrDefault(x => x.Nome == nome);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(resultado);
});

//POST: /api/produto/cadastrar
app.MapPost("/api/produto/cadastrar",
    ([FromBody] Produto produto,
    [FromServices] AppDataContext ctx) =>
{
    //Não permitir o cadastro de um produto
    //com o mesmo nome
    Produto? resultado =
        ctx.Produtos.FirstOrDefault(x => x.Nome == produto.Nome);
    if (resultado is not null)
    {
        return Results.Conflict("Esse produto já existe!");
    }
    ctx.Produtos.Add(produto);
    ctx.SaveChanges();
    return Results.Created("", produto);
});

//DELETE: /api/produto/deletar/id
app.MapDelete("/api/produto/deletar/{id}",
    ([FromRoute] string id,
    [FromServices] AppDataContext ctx) =>
{
    Produto? resultado = ctx.Produtos.Find(id);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encotrado!");
    }
    ctx.Produtos.Remove(resultado);
    ctx.SaveChanges();
    return Results.Ok(resultado);
});

//DELETE: /api/produto/alterar/id
app.MapPatch("/api/produto/alterar/{id}",
    ([FromRoute] string id,
    [FromBody] Produto produtoAlterado,
    [FromServices] AppDataContext ctx) =>
{
    Produto? resultado = ctx.Produtos.Find(id);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    resultado.Nome = produtoAlterado.Nome;
    resultado.Quantidade = produtoAlterado.Quantidade;
    resultado.Preco = produtoAlterado.Preco;
    ctx.Produtos.Update(resultado);
    ctx.SaveChanges();
    return Results.Ok(resultado);
});


//Implementar a remoção e atualização do produto
app.Run();

