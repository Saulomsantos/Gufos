import React, { Component } from 'react';

class Login extends Component {


    render(){
        return(
            <div>
                <main>
                    <section>
                        <p>Bem vindo(a)! <br/> Faça login para acessar sua conta.</p>

                        <form onSubmit>
                            <input 
                                // E-mail
                                type="text"
                                // value
                                // onChange
                                placeholder="username"/>
                            <input 
                                // Senha
                                type="password"
                                // value
                                // onChange
                                placeholder="password"/>
                            <button type="submit">
                                Login
                            </button>
                        </form>
                    </section>
                </main>
            </div>
        )
    }
}

export default Login;