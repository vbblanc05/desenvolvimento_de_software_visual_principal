namespace API.Models;

public class Produto
{
    //C#
    //Construtor
    public Produto()
    {
        Id = Guid.NewGuid().ToString();
        CriadoEm = DateTime.Now;
    }
    //Atributos|Propriedades|Características
    public string Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public double Preco { get; set; }
    public DateTime CriadoEm { get; set; }

    //Java
    // private string nome;
    // public string getNome()
    // {
    //     return "Nome: " + nome;
    // }
    // public void setNome(string nome)
    // {
    //     this.nome = nome + " " + DateTime.Now;
    // }
}
