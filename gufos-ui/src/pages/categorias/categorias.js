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
    buscarCategorias(){
        fetch('https://localhost:5001/api/categorias')

        // Define o tipo de dado do retorno da requisição ( JSON )
        .then(resposta => resposta.json())

        // e atualiza o state listaCategorias com os dados obtidos
        .then(data => this.setState({ listaCategorias : data }))

        // Caso ocorra algum erro, mostra no console do navegador
        .catch((erro) => console.log(erro))
    };

    // Chama a função buscarCategorias() assim que o componente é renderizado
    componentDidMount(){
        this.buscarCategorias();
    };

    // Atualiza o state titulo com o valor do input
    atualizaEstadoTitulo(event){
        this.setState({ titulo : event.target.value })
    };

    // Função responsável por cadastrar uma categoria
    cadastraCategoria(event){

        // Ignora o comportamento padrão do navegador
        event.preventDefault();

        // Caso alguma categoria seja selecionada para editar,
        if (this.state.idCategoriaAlterada !== 0) {

            // faz a chamada para a API usando fetch passando o ID da categoria que será atualizada na URL da requisição
            fetch('https://localhost:5001/api/categorias/' + this.state.idCategoriaAlterada,
            {
                // Define o método da requisição ( PUT )
                method : 'PUT',

                // Define o corpo da requisição especificando o tipo ( JSON )
                body : JSON.stringify({
                    categoriaId : this.state.idCategoriaAlterada,
                    titulo : this.state.titulo
                }),

                // Define o cabeçalho da requisição
                headers : {
                    "Content-Type" : "application/json"
                }
            })

            .then(resposta => {
                // Caso a requisição retorne um status code 204,
                if (resposta.status === 204) {
                    console.log(
                        // Exibe no console do navegador a mensagem 'Categoria x atualizada!' onde x é o ID da categoria atualizada
                        'Categoria ' + this.state.idCategoriaAlterada + ' atualizada!',

                        // e informa qual é seu novo título
                        'Seu novo título agora é: ' + this.state.titulo
                        );
                };
            });
        };

        // Caso nenhuma categoria tenha sido selecionada para editar, realiza o cadastro com a requisição abaixo

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

        .then(resposta => {
            // Caso a requisição retorne um status code 200,
            if (resposta.status === 200) {
                // exibe no console do navegador a mensagem 'Categoria cadastrada!'
                console.log('Categoria cadastrada!');
            };
        })

        // Caso ocorra algum erro,
        // exibe este erro no console do navegador
        .catch(erro => console.log(erro))

        // Então, atualiza a lista de categorias
        // sem o usuário precisar executar outra ação
        .then(this.buscarCategorias)
    };

    // Função responsável por excluir uma categoria
    excluirCategoria = (categoria) => {

        // Exibe no console do navegador o ID da categoria recebida
        console.log('A categoria ' + categoria.categoriaId + ' foi selecionada')

        // Faz a chamada para a API usando fetch passando o ID da categoria recebida na URL da requisição
        fetch('https://localhost:5001/api/categorias/' + categoria.categoriaId,
        {
            // Define o método da requisição ( DELETE )
            method : 'DELETE',
        })

        // Caso a requisição retorne um status code 200,
        .then(resposta => {
            if (resposta.status === 200) {
                // exibe no console do navegador a mensagem 'Categoria x excluída!' onde x é o ID da categoria excluída
                console.log('Categoria ' + categoria.categoriaId + ' excluída!');
            };
        })

        // Caso ocorra algum erro, exibe este erro no console do navegador
        .catch(erro => console.log(erro))

        // Então, atualiza a lista de categorias
        // sem que o usuário precise executar outra ação
        .then(this.buscarCategorias)
    };

    // Recebe uma categoria da lista
    buscarCategoriaPorId = (categoria) => {
        this.setState({ 
            // Atualiza o state idCategoriaAlterada com o valor do ID da categoria recebida
            idCategoriaAlterada : categoria.categoriaId,
            // e o state titulo com o valor do titulo da categoria recebida
            titulo : categoria.titulo
         }, () => {
             console.log(
                // Exibe no console do navegador o valor do ID da categoria recebida,
                'A categoria ' + categoria.categoriaId + ' foi selecionada,',

                // o valor do state idCategoriaAlterada
                'agora o valor do state idCategoriaAlterada é ' + this.state.idCategoriaAlterada,

                // e o valor do state titulo
                'e o valor do state titulo é: ' + this.state.titulo
                );
        });
    };

    // Reseta os states titulo e idCategoriaAlterada
    limparCampos = () => {
        this.setState({
            titulo : '',
            idCategoriaAlterada : 0
        })
        // Exibe no console do navegador a mensagem 'Os states foram resetados!'
        console.log('Os states foram resetados!')
    }

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
                                    this.state.listaCategorias.map((categoria) => {
                                        return (
                                            <tr key={categoria.categoriaId}>
                                                <td>{categoria.categoriaId}</td>
                                                <td>{categoria.titulo}</td>

                                                {/* Faz a chamada da função buscarCategoriaPorId passando a categoria selecionada */}
                                                <td><button onClick={() => this.buscarCategoriaPorId(categoria)}>Editar</button></td>

                                                {/* Faz a chamada da função excluirCategoria passando a categoria selecionada */}
                                                <td><button onClick={() => this.excluirCategoria(categoria)}>Excluir</button></td>
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
                        
                        {/* Formulário de cadastro de categorias */}
                        <form onSubmit={this.cadastraCategoria}>
                            <div>
                                <input
                                    type="text"
                                    value={this.state.titulo}
                                    onChange={this.atualizaEstadoTitulo}
                                    placeholder="Título da categoria"
                                />

                                {/* Altera o texto do botão de acordo com a operação ( edição ou cadastro ) */}

                                {/* 
                                    {
                                        this.state.idCategoriaAlterada === 0 ? 
                                        <button type="submit">Cadastrar</button> : 
                                        <button type="submit">Editar</button>
                                    } 
                                */}

                                {/* Uma outra forma, com if ternário */}

                                {
                                    <button type="submit">
                                        {this.state.idCategoriaAlterada === 0 ? "Cadastrar" : "Atualizar"}
                                    </button>
                                }

                                {/* Faz a chamada da função limparCampos */}
                                
                                <button type="button" onClick={this.limparCampos}>
                                    Reset
                                </button>

                                {/* 
                                    Caso alguma categoria tenha sido selecionada para edição,
                                    exibe a mensagem de feedback ao usuário
                                */}

                                {
                                    this.state.idCategoriaAlterada !== 0 &&
                                    <div>
                                        <p>A categoria {this.state.idCategoriaAlterada} está sendo editada </p>
                                        <p>Clique em Reset caso queira cancelar a operação antes de cadastrar uma nova categoria</p>
                                    </div>
                                }                                
                            </div>
                        </form>
                    </section>
                </main>
            </div>
        );
    }
}

export default Categoria;