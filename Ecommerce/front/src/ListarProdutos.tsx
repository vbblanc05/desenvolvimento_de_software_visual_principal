import { useEffect } from "react";

//Regras para criação de um Componente
//1 - O componente deve seer uma função
//2 - Deve retornar apenas um elemento pai HTML
//3 - Exportar o componente
//4 - O nome da função precisa estar em PascalCasing

function ListarProdutos() {

    //useRffect é metodo que permite executar algum código, no momento do carregamento do componente

    //Pegar os dados que chegaram da requisição e mostrar no HTML
    //Estado/Variável

    useEffect(() => {
        //Bliblioteca AXIOS para requisições
        buscarProdutosAPI();
    }, []);

    async function buscarProdutosAPI() {
        try {
            const resposta = await fetch("http://localhost:5190/api/produto/listar")

            if (!resposta.ok) {
                throw new Error("Requisição com problema: " + resposta.statusText);
            }
            const dados = await resposta.json();
            console.table(dados);
        } catch (error) {
            console.log("Requisição com problema: " + error);
        }
    }

    return (
        <div id="listar_produtos">
            <h1>Listar Produtos</h1>
        </div>
    );
}

export default ListarProdutos;