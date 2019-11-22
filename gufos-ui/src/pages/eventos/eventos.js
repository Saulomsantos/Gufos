import React, { Component } from 'react';
import Axios from 'axios';

class Evento extends Component {
    constructor(props){
        super(props);
        this.state = {
            titulo : '',
            dataEvento : new Date(),
            acessoLivre : 0,
            categoriaId : 0,
            listaCategorias : [],
            listaEventos : [],
            isLoading : false
        }
    }

    // Faz a chamada para a API e atualiza o state listaCategorias com os dados obtidos
    // e exibe no console a lista de categorias
    // Caso ocorra algum erro, mostra no console do navegador
    buscaCategorias(){
        Axios.get('http://localhost:5000/api/categorias')
        .then(resposta => {
            if (resposta.status === 200) {
                this.setState({ listaCategorias : resposta.data })
                console.log(this.state.listaCategorias)
            }
        })
        .catch(erro => console.log(erro))
    }

    // Faz a chamada para a API e atualiza o state listaEventos com os dados obtidos
    // e exibe no console a lista de eventos
    // Caso ocorra algum erro, mostra no console do navegador
    buscarEventos(){
        Axios.get('http://localhost:5000/api/eventos')
        .then(resposta => {
            if (resposta.status === 200) {
                this.setState({ listaEventos : resposta.data })
                console.log(this.state.listaEventos)
            }
        })
        .catch(erro => console.log(erro))
    }

    // Chama as funções buscarCategorias() e buscarEventos() assim que a tela é renderizada
    componentDidMount(){
        this.buscaCategorias();
        this.buscarEventos();
    }

    // Função genérica que atualiza o state de acordo com o input
    // Pode ser reutilizada em vários inputs diferentes
    atualizaStateCampo(event){
        this.setState({ [event.target.name] : event.target.value })
    }

    // Função que faz a chamada para a API para cadastrar um evento
    cadastraEvento(event){
        // Ignora o comportamento padrão do navegador
        event.preventDefault();
        // Define que a requisição está em andamento
        this.setState({ isLoading : true })

        // Define um evento que recebe os dados do state
        // É necessário converter o acessoLivre para int, para que o back-end consiga converter para bool ao cadastrar
        // Como o navegador envia o dado como string, não é possível converter para bool implicitamente
        let evento = {
            titulo : this.state.titulo,
            dataEvento : new Date(this.state.dataEvento),
            acessoLivre : parseInt(this.state.acessoLivre),
            categoriaId : this.state.categoriaId
        };

        // Define a URL e o corpo da requisição
        Axios.post('http://localhost:5000/api/eventos', evento)

        // Verifica o retorno da requisição
        // Caso retorne status code 200, exibe no console do navegador a mensagem 'Evento cadastrado!'
        // e define que a requisição terminou
        .then(data => {
            if (data.status === 200) {
                console.log('Evento cadastrado!')
                this.setState({ isLoading : false })
            }
        })

        // Caso haja algum erro, exibe este erro no console do navegador
        // e define que a requisição terminou
        .catch(erro => {
            console.log(erro)
            this.setState({ isLoading : false })
        })

        // Chama a função buscarCategorias() para atualizar a lista de eventos
        .then(this.buscarEventos.bind(this));
    }

    render(){
        return(
            <div>
                <main>
                    <section>
                        {/* Lista de Eventos */}
                        <h2>Lista de Eventos</h2>
                        <table>
                            <thead>
                                <tr>
                                    <th>#</th>
                                    <th>Evento</th>
                                    <th>Data</th>
                                    <th>Acesso Livre</th>
                                    <th>Categoria</th>
                                </tr>
                            </thead>

                            <tbody>
                                {/* Utilizar função e map para preencher a lista */}
                                {
                                    // Percorre a lista e preenche o corpo da tabela com os dados dos eventos
                                    this.state.listaEventos.map(evento => {
                                        return(
                                            <tr key={evento.eventoId}>
                                                <td>{evento.eventoId}</td>
                                                <td>{evento.titulo}</td>
                                                <td>{evento.dataEvento}</td>
                                                <td>{evento.acessoLivre ? 'Livre' : 'Restrito'}</td>
                                                <td>{evento.categoria.titulo}</td>
                                            </tr>
                                        )
                                    })
                                }
                            </tbody>
                        </table>
                    </section>
                    <section>
                        {/* Cadastro de Eventos */}
                        <h2>Cadastro de Eventos</h2>
                        <form onSubmit={this.cadastraEvento.bind(this)}>
                            <div style={{display: "flex"}}>
                                <input
                                    // Título
                                    type="text"
                                    name="titulo"
                                    value={this.state.titulo}
                                    onChange={this.atualizaStateCampo.bind()}
                                    placeholder="Título do evento"/>
                                <input
                                    // Data do evento
                                    type="date"
                                    name="dataEvento"
                                    value={this.state.dataEvento}
                                    onChange={this.atualizaStateCampo.bind(this)}
                                    placeholder="dd/MM/yyyy"/>
                                <select
                                    // Acesso Livre
                                    name="acessoLivre"
                                    value={this.state.acessoLivre}
                                    onChange={this.atualizaStateCampo.bind(this)}
                                >
                                    <option value="1">Livre</option>
                                    <option value="0">Restrito</option>
                                </select>
                                <select
                                    // Categoria
                                    name="categoriaId"
                                    value={this.state.categoriaId}
                                    onChange={this.atualizaStateCampo.bind(this)}
                                >
                                    <option value="0">Selecione a Categoria</option>

                                    {/* Utilizar função e map para preencher a lista */}

                                    {this.state.listaCategorias.map(categoria => {
                                        return(
                                            <option key={categoria.categoriaId} value={categoria.categoriaId}>
                                                {categoria.titulo}
                                            </option>
                                        );
                                    })}
                                </select>
                                {
                                    this.state.isLoading === true &&
                                    <button type="submit" disabled>Loading...</button>
                                }
                                {
                                    this.state.isLoading === false &&
                                    <button type="submit">Cadastrar</button>
                                }
                            </div>
                        </form>
                    </section>
                </main>
            </div>
        )
    }
}

export default Evento;