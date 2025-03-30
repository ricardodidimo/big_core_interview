# Teste Técnico BigCore - BFF Dashboard - .NET 8 & Vue 3

Este projeto implementa uma arquitetura **BFF (Back-End For Front-End)** utilizando **.NET 8** no back-end e **Vue 3 + Vuetify** no front-end. O objetivo é consumir os dados do microsserviço **Odometer**, aplicar filtros e exibir os dados de forma paginada.

# 📌 Tecnologias Utilizadas
## Backend (.NET 8)
ASP.NET 8 com Service-Repository Pattern

Redis Cache para otimizar chamadas à API

TDD com testes unitários

Containerizado com Docker

Hospedado no Azure com CI/CD (GitHub Actions)

## Frontend (Vue 3)
Vue 3 (Composition API)

Vuetify (UI Framework)

Axios para requisições ao BFF

Internacionalização (i18n)

Persistência de configurações de filtros e colunas

Containerizado com Docker

Hospedado no Azure Static Web Apps

# 🚀 Acessando o projeto live na Azure
[Página Inicial Frontend](https://zealous-field-057c8ff0f.6.azurestaticapps.net/)

[Página documentação API /BFF](https://bigcoreapi.azurewebsites.net/index.html)

# 🚀 Executando o Projeto Localmente com Docker Compose
## Pré-requisitos
Docker e Docker Compose instalados

## Passos
1. Clone o repositório em um diretório de sua preferência
```bash
git clone https://github.com/ricardodidimo/big_core_interview.git
```
2. Na CLI de sua preferência, passe ao diretório do projeto
```bash
cd ./big_core_interview
```

3. No diretório **/src/backend** crie, com ajuda de interface ou via CLI um arquivo nomeado .env no diretório atual com o seguinte conteúdo: 
```text
API_BASE_URL=https://api-elog-client.azurewebsites.net/api/v1/
API_AUTH_HEADER_TOKEN=
```

( ! ) O valor de HEADER TOKEN se refere a chave de autenticação usada pela API eLOG para autenticação BASIC. Preencher a variável 'API_AUTH_HEADER_TOKEN' com o valor integral da chave


4. Suba os containers com Docker Compose
```bash
docker-compose up --build
```

Após isso, o docker subirá três containers: backend, frontend e REDIS. Acesse a aplicação em:

Backend (BFF): http://localhost/index.html

Frontend: http://localhost:5173

# 🚀 Executando o container de testes localmente com Docker Compose
## Pré-requisitos
Docker e Docker Compose instalados

## Passos
1. Clone o repositório em um diretório de sua preferência
```bash
git clone https://github.com/ricardodidimo/big_core_interview.git
```
2. Na CLI de sua preferência, passe ao diretório do projeto
```bash
cd ./big_core_interview
```

3. No diretório **/src/backend** crie, com ajuda de interface ou via CLI um arquivo nomeado .env no diretório atual com o seguinte conteúdo: 
```text
API_BASE_URL=https://api-elog-client.azurewebsites.net/api/v1/
API_AUTH_HEADER_TOKEN=
```

( ! ) O valor de HEADER TOKEN se refere a chave de autenticação usada pela API eLOG para autenticação BASIC. Preencher a variável 'API_AUTH_HEADER_TOKEN' com o valor integral da chave


4. Suba os containers de teste com Docker Compose
```bash
docker-compose -f docker-compose.tests.yml up --build
```
Após isso, o docker irá mostrar no seu console o output da execução do 'dotnet test'
mostrando um laudo sobre os testes executados e o número de acertos e falhas.

( ! ) Após a conclusão dos logs, clique **crtl + c** para sair da execução de testes.

# 📖 Funcionalidades
## ✅ Backend

Exposição de um endpoint BFF que filtra e retorna apenas os dados necessários

Cache em Redis para evitar chamadas desnecessárias à API

Paginação integrada à API

Testes automatizados cobrindo os principais cenários

## ✅ Frontend

Dashboard com tabela dinâmica (reorganização e ocultação de colunas)

Persistência de filtros e configurações do usuário

Internacionalização (i18n)

Paginação e seleção de registros por página

UI usando Vuetify

## Development

### 🚀 CI/CD e Deploy
Pipeline GitHub Actions para build e deploy automático no Azure.
### 🚀 Github
Branches seguem GitHub Flow (feature/xxx → main via Pull Requests).

# 📜 Licença
MIT License.