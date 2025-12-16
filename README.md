# Locadora de Veículos

<p align="left">
  <img src="https://skillicons.dev/icons?i=cs" height="50"/>
  <img src="https://skillicons.dev/icons?i=dotnet" height="50"/>
  <img src="https://skillicons.dev/icons?i=postman" height="50"/>
  <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/microsoftsqlserver/microsoftsqlserver-plain.svg" height="45"/> 
  <img src="https://cdn.jsdelivr.net/gh/simple-icons/simple-icons/icons/newrelic.svg" height="45"/> 
  <img src="https://raw.githubusercontent.com/swagger-api/swagger-ui/master/dist/favicon-32x32.png" width="48"/>
  <img height="50" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/html5/html5-original.svg"/>
  <img height="50" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/sass/sass-original.svg"/>
  <img height="50" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/nodejs/nodejs-original.svg"/>
  <img height="50" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/typescript/typescript-original.svg"/>
  <img height="50" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/bootstrap/bootstrap-original.svg"/>
  <img height="50" src="https://raw.githubusercontent.com/devicons/devicon/master/icons/microsoftsqlserver/microsoftsqlserver-plain.svg"/>
  <img height="50" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/angular/angular-original.svg"/>
  <img height="50" src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/rxjs/rxjs-original.svg"/>
</p>

## Projeto

Desenvolvido durante o curso Full-Stack da Academia do Programador 2025, este backend implementa toda a lógica de domínio, regras de negócio, autenticação e persistência para o sistema de Locadora de Veículos.

## Descrição

O sistema gerencia os principais processos de uma locadora, permitindo o controle completo de clientes, condutores, funcionários, automóveis, planos de cobrança e locações.  
A aplicação segue arquitetura em camadas (Domínio, Aplicação, Infra e WebAPI) com foco em organização e boas práticas.

## Funcionalidades

1. **Funcionários**
   - Nome
   - Login
   - Cargo
   - Permissões

2. **Clientes**
   - Nome
   - Endereço completo
   - Telefone
   - Documento
   - Tipo de cliente (PF/PJ)
   - CNH (quando aplicável)

3. **Condutores**
   - Nome
   - Documento
   - CNH
   - Cliente vinculado

4. **Automóveis**
   - Modelo
   - Marca
   - Ano
   - Placa
   - Quilometragem
   - Tipo de combustível
   - Grupo de veículos

5. **Planos de Cobrança**
   - Plano Diário
   - Km Controlado
   - Km Livre

6. **Locações**
   - Cliente
   - Condutor
   - Automóvel
   - Plano selecionado
   - Datas de início e término
   - Taxas e adicionais

## Requisitos para Execução

- .NET SDK (recomendado .NET 8.0 ou superior) para compilação e execução do projeto back-end.
- Node.js v20+

## Configuração de Variáveis de Ambiente

Configurar via **dotnet user-secrets** dentro do projeto WebAPI:

```json
{
  "SQL_CONNECTION_STRING": "{substitua-pelo-segredo}",
  "NEWRELIC_LICENSE_KEY": "{substitua-pelo-segredo}",
  "JWT_GENERATION_KEY": "{substitua-pelo-segredo}",
  "JWT_AUDIENCE_DOMAIN": "https://localhost:4200/",
  "CORS_ALLOWED_ORIGINS": "http://localhost:4200"
}
```

## Executando o Back-End

Vá para a pasta do projeto da WebAPI:

```bash
cd server/web-api
```

Execute o projeto:

```bash
dotnet run
```

A API poderá ser acessada no endereço `https://localhost:59251/api`.

A documentação **OpenAPI** também estará disponível em: `https://localhost:59251/swagger`.

## Executando o Front-End

Vá para a pasta do projeto Angular:

```bash
cd client
```

Instale as dependências:

```bash
npm install
```

Execute o projeto:

```bash
npm start
```

A aplicação Angular estárá disponível no endereço `http://localhost:4200`.
