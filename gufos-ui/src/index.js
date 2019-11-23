import React from 'react';
import ReactDOM from 'react-dom';
import { Route, BrowserRouter as Router, Switch, Redirect } from 'react-router-dom';
import { usuarioAutenticado, parseJwt } from './services/auth';

import './index.css';

import App from './pages/home/App';
import Categorias from './pages/categorias/categorias';
import Eventos from './pages/eventos/eventos';
import Login from './pages/login/login';

import * as serviceWorker from './serviceWorker';

// Verifica se o usuário está logado e se é 'Administrador'
const PermissaoAdm = ({ component : Component }) => (
    <Route
        render = { props => 
            usuarioAutenticado() && parseJwt().Role === 'Administrador' ? (
                // Se sim, renderiza de acordo com a rota solicitada e permitida
                <Component {...props} />
            ) : (
                // Se não, redireciona para a página de login
                <Redirect to = {{ pathname : 'login' }} />
            )
        }
    />
);

// Verifica se o usuário está logado e se é 'Aluno'
const PermissaoAluno = ({ component : Component }) => (
    <Route
        render = { props => 
            usuarioAutenticado() && parseJwt().Role === 'Aluno' ? (
                // Se sim, renderiza de acordo com a rota solicitada e permitida
                <Component {...props} />
            ) : (
                // Se não, redireciona para a página de login
                <Redirect to = {{ pathname : 'login' }} />
            )
        }
    />
);

// Define a constante routing que irá renderizar as páginas de acordo com a rota
const routing = (
    <Router>
        <div>
            <Switch>
                <Route exact path="/" component={App} /> {/* Home */}
                <PermissaoAdm path="/categorias" component={Categorias} /> {/* Categorias */}
                <PermissaoAluno path="/eventos" component={Eventos} /> {/* Eventos */}
                <Route path="/login" component={Login} /> {/* Login */}
            </Switch>
        </div>
    </Router>
);

// Renderiza as rotas no body do index.html
ReactDOM.render(routing, document.getElementById('root'));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
