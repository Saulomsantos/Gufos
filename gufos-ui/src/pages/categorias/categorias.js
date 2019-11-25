import React, { Component } from 'react';

class Categoria extends Component {
    constructor(props){
        super(props);
        this.state = {
            listaCategorias : [],
            titulo : '',
            idCategoriaAlterada : 0
        };

        // Declara os estados das funções para que possam ser chamadas no componente
        this.atualizaEstadoTitulo = this.atualizaEstadoTitulo.bind(this);
        this.cadastraCategoria = this.cadastraCategoria.bind(this);
        this.buscarCategorias = this.buscarCategorias.bind(this);
    };

    // Faz a chamada para a API usando fetch
    // e atualiza o state listaCategorias com os dados obtidos
    // Caso ocorra algum erro, mostra no console do navegador
    buscarCategorias(){
        fetch('https://localhost:5001/api/categorias')
        .then(resposta => resposta.json())
        .then(data => this.setState({ listaCategorias : data }))
        .catch(erro => console.log(erro));
    };

    buscarCategoriaPorId = (categoria) => {
        this.setState({ 
            idCategoriaAlterada : categoria.categoriaId,
            titulo : categoria.titulo
         }, () => {
            console.log(categoria.categoriaId, this.state.idCategoriaAlterada)
        });
    }

    // Chama a função buscarCategorias() assim que a tela é renderizada
    componentDidMount(){
        this.buscarCategorias();
    };

    // Atualiza o state titulo com o valor do input
    atualizaEstadoTitulo(event){
        this.setState({ titulo : event.target.value });
    };

    // Função responsável por cadastrar uma categoria
    cadastraCategoria(event){
        
        // Ignora o comportamento padrão do navegador
        event.preventDefault();

        fetch('https://localhost:5001/api/categorias/' + this.state.idCategoriaAlterada)
        .then(resposta => {
            if (resposta.data === null) {
                // Faz a chamada para a API usando fetch
                fetch('https://localhost:5001/api/categorias',
                {
                    // Define o método da requisição ( POST )
                    method : 'POST',
                    // Define o corpo da requisição especificando o tipo ( JSON )
                    body : JSON.stringify({ titulo : this.state.titulo }),
                    // Define o cabeçalho da requisição
                    headers : {
                        "Content-Type" : "application/json"
                    }
                })

                // Caso a requisição retorne um status code 200,
                // exibe no console do navegador a mensagem 'Categoria cadastrada!'
                .then(resposta => {
                    if (resposta.status === 200) {
                        console.log('Categoria cadastrada!');
                    };
                })

                // Caso ocorra algum erro,
                // exibe este erro no console do navegador
                .catch(erro => console.log(erro))

                // Então, atualiza a lista de categorias
                // sem o usuário precisar executar outra ação
                .then(this.buscarCategorias);
            }
            else {
                console.log(
                    this.state.idCategoriaAlterada,
                    this.state.titulo
                )
                fetch('https://localhost:5001/api/categorias/' + this.state.idCategoriaAlterada, 
                {
                    method : 'PUT',
                    body : JSON.stringify({
                        categoriaId : this.state.idCategoriaAlterada,
                        titulo: this.state.titulo
                    })
                })
                .then(resposta => {
                    if (resposta.status === 200) {
                        console.log('Categoria atualizada!');
                    };
                })
            }
        })

        
    };

    render(){
        return(
            <div>
                <main>
                    <section>
                        {/* Lista de Categorias */}
                        <h2>Lista de Categorias</h2>
                        <table>
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Título</th>
                                    <th>Ações</th>
                                </tr>
                            </thead>

                            <tbody>
                                {/* Utilizar função e map para preencher a lista */}
                                {
                                    // Percorre a lista e preenche o corpo da tabela com o ID 
                                    // e o Título de cada categoria
                                    this.state.listaCategorias.map((categoria) => {
                                        return (
                                            <tr key={categoria.categoriaId}>
                                                <td>{categoria.categoriaId}</td>
                                                <td>{categoria.titulo}</td>
                                                <td>
                                                    {/* Faz a chamada para a função  */}
                                                    <button onClick={ () => { this.buscarCategoriaPorId(categoria) } }>Editar</button>
                                                    <button>Excluir</button>
                                                </td>
                                            </tr>
                                        );
                                    })
                                }
                            </tbody>
                        </table>
                    </section>
                    <section>
                        {/* Cadastro de Categorias */}
                        <h2>Cadastro de Categorias</h2>
                        {/* Faz a chamada para a função de cadastro quando o botão é pressionado */}
                        <form onSubmit={this.cadastraCategoria}>
                            <div>
                                <input
                                    type="text"
                                    // Define que o valor do input é o valor do state
                                    value={this.state.titulo}
                                    // Chama a função para atualizar o state
                                    // cada vez que há uma alteração no input
                                    onChange={this.atualizaEstadoTitulo}
                                    placeholder="Título da categoria"
                                />
                                <button type="submit">
                                    Cadastrar
                                </button>
                            </div>
                        </form>
                    </section>
                </main>
            </div>
        );
    }
}

export default Categoria;