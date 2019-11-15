import React, { Component } from 'react';

class Categoria extends Component {
    constructor(props){
        super(props);
        this.state = {
            listaCategorias : [],
            titulo : ''
        }
    }

    // Faz a chamada para a api e atualiza o state listaCategorias com os dados obtidos
    // Caso ocorra algum erro, mostra no console do navegador
    buscarCategorias(){
        fetch('http://localhost:5000/api/categorias')
        .then(resposta => resposta.json())
        .then(data => this.setState({ listaCategorias : data }))
        .catch(erro => console.log(erro))
    }

    // Chamada a função buscarCategorias() assim que a tela é renderizada
    componentDidMount(){
        this.buscarCategorias();
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
                        <form onSubmit>
                            <div>
                                <input
                                    type="text"
                                    // value=
                                    // onChange=
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