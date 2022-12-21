# API Blog com Entity Framework

![Badge](http://img.shields.io/static/v1?label=STATUS&message=DEVELOPMENT&color=yellow&style=for-the-badge)

<p align="center">CRUD de uma API de um Blog utilizando boas práticas de desenvolvimento<p>

## Funcionalidades
- Cadastro de usuário
- Confirmação via email (necessario gerar e configurar nova Key no sendgrid)
- Tratamento de erros
- CRUD

## Endpoints

| Método | Path | Descrição |
|---|---|---|
| `POST` | v1/accounts | Cadastra um novo usuário. |
| `POST` | v1/login | Faz login. |
| `GET` | v1/user | Busca o registro de todos usuarios. |
| `GET` | v1/user/{id} | Busca o registro de um unico usuario. |
| `PUT` | v1/user/{id} | Atualiza o registro de um usuario. |
| `DELETE` | v1/user/{id} | Deleta o registro de um usuario. |
| `GET` | v1/categories | Busca o registro de todas categorias. |
| `GET` | v1/categories/{id} | Busca o registro de uma categoria. |
| `POST` | v1/categories | Cadastra uma categoria. |
| `PUT` | v1/categories/{id}| Faz a atualização dos dados de uma categoria. |
| `DELETE` | v1/categories/{id}| Faz a deleção dos dados de uma categoria. |


## Tecnologias utilizadas
- JwtBearer
- Authentication
- EntityFrameworkCore.Design
- SqlServer
- SecureIdentity
- sendgrid
