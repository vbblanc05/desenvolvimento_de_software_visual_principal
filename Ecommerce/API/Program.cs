using API.Models;
using Microsoft.AspNetCore.Mvc;

Console.Clear();
var builder = WebApplication.CreateBuilder(args);
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

// Métodos HTTP:
// GET    - Recupera dados do servidor
// POST   - Envia/Cadastrar dados para criar um novo recurso
// PUT    - Atualiza um recurso existente
// DELETE - Remove um recurso
// PATCH  - Atualiza parcialmente um recurso

// Os códigos de status HTTP são divididos em cinco classes:
// 1xx (Informativa)
// 100 Continue: O servidor recebeu a solicitação inicial e o cliente deve continuar a enviar o restante da requisição. 
// 2xx (Sucesso)
// 200 OK: A requisição foi bem-sucedida. É o código de status mais comum para uma resposta bem-sucedida.
// 201 Created: A requisição foi bem-sucedida e um novo recurso foi criado.
// 204 No Content: A requisição foi bem-sucedida, mas não há conteúdo para ser enviado na resposta. 
// 3xx (Redirecionamento)
// 301 Moved Permanently: O recurso solicitado foi permanentemente movido para uma nova URL.
// 302 Found: O recurso solicitado foi temporariamente movido para uma nova URL. 
// 4xx (Erro do Cliente)
// 400 Bad Request: A requisição do cliente foi malformada ou inválida.
// 401 Unauthorized: A requisição requer autenticação. O cliente precisa fornecer credenciais válidas.
// 403 Forbidden: O cliente não tem permissão para acessar o recurso, mesmo que a autenticação tenha sido fornecida.
// 404 Not Found: O servidor não conseguiu encontrar o recurso solicitado.
// 429 Too Many Requests: O cliente enviou muitas solicitações em um determinado período de tempo. 
// 5xx (Erro do Servidor)
// 500 Internal Server Error: O servidor encontrou uma condição inesperada que o impediu de atender à requisição.
// 503 Service Unavailable: O servidor não está pronto para lidar com a requisição, geralmente devido a uma sobrecarga ou manutenção.
// 504 Gateway Timeout: O servidor, que estava atuando como um gateway, não recebeu uma resposta dentro do tempo limite. 

app.MapGet("/", () => "API de Produtos");

//GET: /api/produto/listar
app.MapGet("/api/produto/listar", () =>
{
    //Validar a lista de produtos para saber se existe algo dentro
    if (produtos.Any())
    {
        return Results.Ok(produtos);
    }

    return Results.NotFound("Lista vazia!");
});

//GET: /api/produto/buscar/produto_buscado
app.MapGet("/api/produto/buscar/{nome}", (string nome) =>
{
    //Expressão lambda
    Produto? resultado = produtos.FirstOrDefault(x => x.Nome == nome);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    return Results.Ok(resultado);
});

//POST: /api/produto/cadastrar
app.MapPost("/api/produto/cadastrar",
    ([FromBody] Produto produto) =>
{
    //Não permitir o cadastro de um produto com mesmo nome
    foreach (Produto produtoCadastrado in produtos)
    {
        if (produtoCadastrado.Nome == produto.Nome)
        {
            return Results.Conflict("Produto já cadastrado!");
        }
    }
    produtos.Add(produto);
    return Results.Created("", produto);
});

//DELETE: /api/produto/alterar/id
app.MapPatch("/api/produto/alterar/{id}",
    ([FromRoute] string id,
    [FromBody] Produto produtoAlterado) =>
{
    Produto? resultado = produtos.FirstOrDefault
        (produtoCadastrado => produtoCadastrado.Id == id);
    if (resultado is null)
    {
        return Results.NotFound("Produto não encontrado!");
    }
    resultado.Nome = produtoAlterado.Nome;
    resultado.Quantidade = produtoAlterado.Quantidade;
    resultado.Preco = produtoAlterado.Preco;
    return Results.Ok(resultado);
});

//Implementar a remoção e atualização do produto
app.Run();