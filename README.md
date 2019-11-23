# Gufos

Arquivos referentes ao projeto Gufos que foi desenvolvido em conjunto com os alunos das turmas A e B do curso CodeXP no segundo semestre de 2019



## Descrição

O projeto Gufos é um sistema de gerenciamento de eventos

Nele, é possível que o aluno visualize os eventos disponíveis, com data, local e se o acesso é livre ou privado

O administrador do sistema gerencia todas as informações como listagem, cadastro, atualização e exclusão de usuários, categorias às quais os eventos pertencem, localizações e os eventos propriamente ditos



## Tecnologias

### Banco de dados

O banco de dados do projeto Gufos foi desenvolvido através das modelagens: conceitual, lógica e física

Após a modelagem, foram gerados os scripts de criação (DDL), manipulação (DML) e consulta (DQL) utilizando MSSQL como linguagem

### Back-end

O back-end do projeto Gufos foi desenvolvido construindo uma Web API utilizando C# como linguagem fornecendo e recebendo dados no formato JSON

Foi utilizado o ORM Entity Framework com o método Database First

A arquitetura da aplicação segue o padrão REST e os endpoints estão documentados através do Swagger

A estrutura foi dividida entre Domains, Controllers, Interfaces e Repositories

Implementada autenticação e autorização utilizando JWT

#### Observação

A API desenvolvida com .NET Core 3.0 (./backend) está habilitada para requisições https usando a porta :5001

A API desenvolvida com .NET Core 2.1 (./backend_2.1) está habilitada para requisições http usando a porta :5000

### Front-end

O front-end do projeto Gufos foi desenvolvido em JavaScript utilizando a biblioteca React.Js
