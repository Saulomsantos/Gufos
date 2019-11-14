import React, { Component } from 'react';

class Evento extends Component {


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
                            </tbody>
                        </table>
                    </section>
                    <section>
                        {/* Cadastro de Eventos */}
                        <h2>Cadastro de Eventos</h2>
                        <form onSubmit>
                            <div style={{display: "flex"}}>
                                <input
                                    // Título
                                    type="text"
                                    // value
                                    // onChange
                                    placeholder="Título do evento"/>
                                <input
                                    // Data do evento
                                    type="date"
                                    // value
                                    // onChange
                                    placeholder="dd/MM/yyyy"/>
                                <select
                                    // Acesso Livre
                                    // value
                                    // onChange
                                >
                                    <option value="1">Livre</option>
                                    <option value="0">Restrito</option>
                                </select>
                                <select
                                    // Categoria
                                    // value
                                    // onChange
                                >
                                    <option value="0">Selecione a Categoria</option>
                                    <option>{/* Utilizar função e map para preencher a lista */}</option>
                                </select>
                                <button type="submit">
                                    Cadastrar
                                </button>
                            </div>
                        </form>
                    </section>
                </main>
            </div>
        )
    }
}

export default Evento;