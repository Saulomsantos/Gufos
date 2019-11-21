import React, { Component } from 'react';

class Categoria extends Component {
    constructor(props){
        super(props);
        this.state = {
            listaCategorias : [],
            titulo : ''
        }

        this.atualizaEstadoTitulo = this.atualizaEstadoTitulo.bind(this);
        this.cadastraCategoria = this.cadastraCategoria.bind(this);
        this.buscarCategorias = this.buscarCategorias.bind(this);
    }

    // Faz a chamada para a api e atualiza o state listaCategorias com os dados obtidos
    // Caso ocorra algum erro, mostra no console do navegador
    buscarCategorias(){
        fetch('http://localhost:5000/api/categorias')
        .then(resposta => resposta.json())
        .then(data => this.setState({ listaCategorias : data }))
        .catch(erro => console.log(erro))
    }

    // Chama a função buscarCategorias() assim que a tela é renderizada
    componentDidMount(){
        this.buscarCategorias();
    }

    // Atualiza o state titulo com o valor do input
    atualizaEstadoTitulo(event){
        this.setState({ titulo : event.target.value })
    }

    cadastraCategoria(event){
        event.preventDefault();

        fetch('http://localhost:5000/api/categorias',
        {
            method : 'POST',
            body : JSON.stringify({ titulo : this.state.titulo }),
            headers : {
                "Content-Type" : "application/json"
            }
        })
        .then(resposta => {
            if (resposta.status === 200) {
                console.log('Categoria cadastrada!');
            }
        })
        .catch(erro => console.log(erro))
        .then(this.buscarCategorias);
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
                                </tr>
                            </thead>

                            <tbody>
                                {/* Utilizar função e map para preencher a lista */}
                                {
                                    // Percorre a lista e preenche o corpo da tabela com o ID e o Título
                                    // de cada categoria
                                    this.state.listaCategorias.map(function(categoria){
                                        return (
                                            <tr key={categoria.categoriaId}>
                                                <td>{categoria.categoriaId}</td>
                                                <td>{categoria.titulo}</td>
                                            </tr>
                                        )
                                    })
                                }
                            </tbody>
                        </table>
                    </section>
                    <section>
                        {/* Cadastro de Categorias */}
                        <h2>Cadastro de Categorias</h2>
                        <form onSubmit={this.cadastraCategoria}>
                            <div>
                                <input
                                    type="text"
                                    value={this.state.titulo}
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