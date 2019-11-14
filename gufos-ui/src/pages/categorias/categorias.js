import React, { Component } from 'react';

class Categoria extends Component {
    

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