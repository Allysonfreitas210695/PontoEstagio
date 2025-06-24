# PontoEstagio - Sistema de Controle de Ponto para Estagiários

![DotNet](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

![Docker](https://img.shields.io/badge/docker-%230db7ed.svg?style=for-the-badge&logo=docker&logoColor=white)

![SQL Server](https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)

Solução completa para gerenciamento de ponto eletrônico para estagiários, com autenticação, registro de horas e relatórios.

## 📋 Principais Características

- Arquitetura limpa com separação de responsabilidades
- Autenticação via JWT
- CRUD completo de estagiários
- Registro de horas trabalhadas
- Geração de relatórios
- Testes unitários e de integração
- Pronto para Docker

## 🛠️ Tecnologias Utilizadas

- .NET 8
- Entity Framework Core
- SQL Server
- FluentValidation
- Swagger
- JWT Authentication
- BCrypt para hashing de senhas
- Docker
- xUnit (para testes)
- Moq (para mocks em testes)
- Bogus (para geração de dados fake)

## 🚀 Como Executar o Projeto

### Pré-requisitos

- .NET 8 SDK
- Docker (opcional, para rodar em containers)
- SQL Server (pode ser substituído pelo SQL Server em container)

 🐋 Executando com Docker

 1. Certifique-se que o Docker está instalado e rodando

 2. Execute:

 ```bash
    docker compose up -d
 ```

 2. Verifique os containers em execução:

```bash
    docker ps
 ```
 
 📊 Estrutura do Projeto

 PontoEstagio/
 ```bash
     ├── src/
     │   ├── PontoEstagio.API/          # Camada de apresentação (Web API)
     │   ├── PontoEstagio.Application/  # Regras de negócio e casos de uso
     │   ├── PontoEstagio.Domain/       # Entidades e contratos do domínio
     │   ├── PontoEstagio.Infrastructure/ # Implementações de infra (banco, serviços externos)
     │   ├── PontoEstagio.Exceptions/   # Exceções customizadas
     │   └── PontoEstagio.Communication/ # DTOs e contratos de comunicação
     ├── tests/
     │   ├── PontoEstagio.CommonTestUltilities/   # Mocks
     │   └── PontoEstagio.Validators/ # Testes Unidade
     └── PontoEstagio.sln               # Solução
```