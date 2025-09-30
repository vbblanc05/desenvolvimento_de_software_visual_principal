using System;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

//Classe que representa o banco de dados da aplicação
//1 - Criar a herança da classe DbContext
//2 - Informar quais as classes vão representar as tabelas no banco
//3 - Configurar a string de conexão para SQLite
public class AppDataContext : DbContext
{
    //Atributos representam as tabelas no banco
    public DbSet<Produto> Produtos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Ecommerce.db");
    }
}