using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
