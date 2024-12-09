# Gerenciador de Produtos API

A API **Gerenciador de Produtos** permite o gerenciamento de produtos e categorias em um sistema de estoque. Ela oferece funcionalidades para criação, atualização, exclusão e listagem de produtos, além de controle de acesso baseado em papéis de usuários (ex: Gerente, Funcionário). A API também disponibiliza a listagem de produtos em estoque.

## Tecnologias Utilizadas

- **ASP.NET Core** - Framework para desenvolvimento de APIs.
- **Entity Framework Core** - ORM para manipulação de banco de dados.
- **SQL Server** - Banco de dados relacional.
- **JWT (JSON Web Tokens)** - Autenticação baseada em tokens para segurança.
- **Swagger** - Documentação interativa da API.

## Como Obter o Token de Autenticação

Para acessar a API, é necessário autenticar-se utilizando um token JWT.

### Curl para Obter o Token

curl --location 'http://localhost:5010/api/Auth/login' \
--header 'Content-Type: application/json' \
--data '{
    "username": "gerente",
    "password": "123456"
}'
## Exemplo curl para criação de produto

curl --location 'http://localhost:5010/api/Produto' \
--header 'Authorization: Bearer (token)
--header 'Content-Type: application/json' \
--data '{
    "nome": "Produto 1",
    "descricao": "Descrição do Produto 1",
    "preco": 100.00,
    "quantidadeEstoque": 10,
    "status": "Em estoque",
    "categoriaId": 1
}

## Exemplo de Header para Autenticação

Authorization: Bearer {token}


## Funcionalidades

### Produtos

- **GET /api/produto** - Lista todos os produtos cadastrados.
- **GET /api/produto/em-estoque** - Lista todos os produtos que estão em estoque.
- **POST /api/produto** - Cria um novo produto.
- **PUT /api/produto/{id}** - Atualiza um produto existente.
- **DELETE /api/produto/{id}** - Exclui um produto existente.

### Categorias

- **GET /api/categoria** - Lista todas as categorias cadastradas.
- **POST /api/categoria** - Cria uma nova categoria.
- **PUT /api/categoria{id}** - Atualiza uma categoria existente.
- **DELETE /api/categoria/{id}** - Exclui uma categoria existente.

## Autenticação e Autorização

A API utiliza **JWT (JSON Web Tokens)** para autenticação e controle de acesso.

### Papéis de Usuário

- **Gerente**: Pode acessar todos os endpoints e realizar todas as operações (criar, atualizar, excluir).
- **Funcionário**: Pode acessar os endpoints de listagem de produtos e atualizar produtos.



## 👩‍💻 Como Rodar o Projeto

1. Clone o repositório.

2. Configure o banco de dados no arquivo `appsettings.json` com a seguinte string de conexão:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=SEUBANCODEDADOS;Database=GerenciadorDeProdutosDB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;"
   }
   ```

• Execute as migrações com o Entity Framework para criar as tabelas no banco de dados.

• Execute o projeto através do Visual Studio ou linha de comando.

Configurar o Banco de Dados Criar o Banco de Dados

Execute o script da pasta ScriptBanco no SQL Server Management Studio (SSMS) para criar as tabelas.
