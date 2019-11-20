import React, { Component } from 'react';
import Axios from 'axios';

class Login extends Component {
    constructor(props){
        super(props);
        this.state = {
            email : '',
            senha : '',
            erroMensagem : ''
        }
    }

    // Função que faz a chamada para a API para realizar o login
    efetuaLogin(event){
        // Ignora o comportamento padrão do navegador
        event.preventDefault();

        // Define a URL e o corpo da requisição
        Axios.post('http://localhost:5000/api/login',{
            email : this.state.email,
            senha : this.state.senha
        })

        // Verifica o retorno da requisição
        // Caso seja status code 200, salva o token no localStorage
        // e exibe o token no console do navegador
        .then(data => {
            if (data.status === 200) {
                localStorage.setItem('usuario-gufos', data.data.token);
                console.log('Meu token é: ' + data.data.token);
            }
        })

        // Caso haja um erro, define o state erroMensagem como 'E-mail ou senha inválidos!'
        .catch(erro => {
            this.setState({ erroMensagem : 'E-mail ou senha inválidos!' })
        })
    }

    // Função genérica que atualiza o state de acordo com o input
    // Pode ser reutilizada em vários inputs diferentes
    atualizaStateCampo(event){
        this.setState({ [event.target.name] : event.target.value })
    }


    render(){
        return(
            <div>
                <main>
                    <section>
                        <p>Bem vindo(a)! <br/> Faça login para acessar sua conta.</p>

                        {/* Faz a chamada para a função de login quando o botão é pressionado */}
                        <form onSubmit={this.efetuaLogin.bind(this)}>
                            <input 
                                // E-mail
                                type="text"
                                name="email"
                                // Define que o input email recebe o valor do state email
                                value={this.state.email}
                                // Faz a chamada para a função que atualiza o state, conforme
                                // o usuário altera o valor do input
                                onChange={this.atualizaStateCampo.bind(this)}
                                placeholder="username"/>
                            <input 
                                // Senha
                                type="password"
                                name="senha"
                                // Define que o input senha recebe o valor do state senha
                                value={this.state.senha}
                                // Faz a chamada para a função que atualiza o state, conforme
                                // o usuário altera o valor do input
                                onChange={this.atualizaStateCampo.bind(this)}
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