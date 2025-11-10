import { useEffect, useState } from "react";

//Regras para criação de um componente
//1 - Componente deve ser uma função
//2 - Retorne apenas um elemento pai HTML
//3 - Exportar o componente para que ele
//seja utilizado na aplicação

function ListarProdutos() {
  //Estados | Variáveis
  //const [nome, setNome] = useState("Diogo Steinke Deconto");
  const [produtos, setProdutos] = useState<[]>([]);

  //useEffect é utilizado para executar algum código no
  //momento em que o componente é carregado no navegador
  useEffect(() => {
    listarProdutosAPI();
  }, []);

  async function listarProdutosAPI() {
    //AXIOS - Biblioteca para realizar requisições
    try {
      const resposta = await fetch(
        "http://localhost:5190/api/produto/listar"
      );
      if (!resposta.ok) {
        throw new Error("Erro na requisição: " + resposta.statusText);
      }
      const dados = await resposta.json();
      setProdutos(dados);
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <div id="componente_listar_produtos">
      <h1>Listar Produtos</h1>
      <table>
        <thead>
          <tr>
            <th>#</th>
            <th>Nome</th>
            <th>Preço</th>
            <th>Quantidade</th>
            <th>Criado em</th>
          </tr>
        </thead>
        <tbody>
          {produtos.map((produto: any) => (
            <tr>
              <td>{produto.id}</td>
              <td>{produto.nome}</td>
              <td>{produto.preco}</td>
              <td>{produto.quantidade}</td>
              <td>{produto.criadoEm}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ListarProdutos;
