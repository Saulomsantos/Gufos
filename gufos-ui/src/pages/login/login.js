import React, { Component } from 'react';
import Axios from 'axios';
import { parseJwt } from '../../services/auth';

class Login extends Component {
    constructor(props){
        super(props);
        this.state = {
            email : '',
            senha : '',
            erroMensagem : '',
            isLoading : false
        };
    };

    // Função que faz a chamada para a API para realizar o login
    efetuaLogin(event){
        // Ignora o comportamento padrão do navegador
        event.preventDefault();

        // Remove a frase de erro do state erroMensagem
        this.setState({ erroMensagem : '' });

        // Define que a requisição está em andamento
        this.setState({ isLoading : true });

        // Define a URL e o corpo da requisição
        Axios.post('https://localhost:5001/api/login',{
            email : this.state.email,
            senha : this.state.senha
        })

        // Verifica o retorno da requisição
        .then(data => {
            // Caso seja status code 200,
            if (data.status === 200) {
                // salva o token no localStorage,
                localStorage.setItem('usuario-gufos', data.data.token);
                // exibe o token no console do navegador
                console.log('Meu token é: ' + data.data.token);
                // e define que a requisição terminou
                this.setState({ isLoading : false });

                // Define a variável base64 que vai receber o payload do token
                var base64 = localStorage.getItem('usuario-gufos').split('.')[1];
                // Exibe no console o valor presente na variável base64
                console.log(base64);
                // Exibe no console o valor convertido da base64 para string
                console.log(window.atob(base64));
                // Exibe no console o valor convertido da string para JSON
                console.log(JSON.parse(window.atob(base64)));
                
                // Exibe no console apenas o tipo de usuário logado
                console.log(parseJwt().Role);

                // Verifica se o tipo de usuário logado é Administrador
                // Se for, redireciona para a página de categorias
                if (parseJwt().Role === 'Administrador') {
                    this.props.history.push('/categorias');
                }
                // Se não for, redireciona para a página de eventos
                else {
                    this.props.history.push('/eventos');
                };
            };
        })

        // Caso haja um erro,
        .catch(erro => {
            // define o state erroMensagem como 'E-mail ou senha inválidos!'
            this.setState({ erroMensagem : 'E-mail ou senha inválidos!' });
            // e define que a requisição terminou
            this.setState({ isLoading : false });
        });
    };

    // Função genérica que atualiza o state de acordo com o input
    // Pode ser reutilizada em vários inputs diferentes
    atualizaStateCampo(event){
        this.setState({ [event.target.name] : event.target.value });
    };

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

                            {/* Exibe a mensagem de erro ao tentar logar com credenciais inválidas */}
                            <p style={{ color : 'red' }}>{this.state.erroMensagem}</p>

                            {/* 
                                Verifica se a requisição está em andamento
                                Se estiver, desabilita o click do botão
                            */}                            
                            {
                                // Caso seja true, renderiza o botão desabilitado com o texto 'Loading...'
                                this.state.isLoading === true &&
                                <button type="submit" disabled>Loading...</button>
                            }
                            {
                                // Caso seja false, renderiza o botão habilitado com o texto 'Login'
                                this.state.isLoading === false &&
                                <button
                                    type="submit"
                                    disabled={this.state.email === '' || this.state.senha === '' ? 'none' : ''}>
                                        Login
                                </button>
                            }
                        </form>
                    </section>
                </main>
            </div>
        );
    };
};

export default Login;