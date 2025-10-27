using System;

namespace Auth.Models;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public DateTime CriadoEm { get; set; } = DateTime.Now;
}
