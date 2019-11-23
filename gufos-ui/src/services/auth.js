// Define a constante usuarioAutenticado que recebe o token armazenado no localStorage
export const usuarioAutenticado = () => localStorage.getItem('usuario-gufos') !== null;

// Define a constante parseJwt que retorna o payload do usuário convertido em JSON
export const parseJwt = () => {
    // Define a variável base64 que recebe o payload do usuário logado codificado
    var base64 = localStorage.getItem('usuario-gufos').split('.')[1];

    // Decodifica a base64 para string, através do método atob
    // e converte a string para JSON
    return JSON.parse(window.atob(base64));
}