# 📘 PontoEstágio

**PontoEstágio** é um sistema de controle de frequência e atividades desenvolvido para estagiários, estudantes ou membros de projetos. Ele permite o registro diário de presença e tarefas, além de possibilitar o acompanhamento e validação por parte de supervisores.

---

## 🚀 Funcionalidades Principais

### 🧑‍💼 Cadastro de Usuários
- [x] Registro de **estagiários** e **supervisores**

### 🏗️ Cadastro de Projetos
- [x] Criação de **projetos**
- [x] Associação de estagiários/supervisores ao projeto

### 🕒 Registro de Frequência
- [x] Registro de **entrada e saída** com data/hora
- [x] Frequência salva com status "**pendente**"

### 📋 Registro de Atividades
- [] Adição de **descrição das atividades realizadas**
- [] Upload opcional de **comprovantes** ou anexos
- [] Ligação direta entre atividade e frequência do dia
- [] Histórico de atividades por data

### ✅ Validação pelo Supervisor
- [ ] Visualização de registros pendentes
- [ ] Aprovação ou reprovação de presença e atividade

### 📊 Relatórios
- [x] Geração de relatório **mensal** por estagiário
- [x] Cálculo de **horas totais** de presença

---

## 🛠️ Tecnologias Utilizadas

### Back-end:
- C# .NET Core
- SQL Server
- API REST com autenticação via JWT
- Swagger para testes de endpoints

### Front-end:
- Next.js + TypeScript
- TailwindCSS
- React Hook Form para formulários
- Axios para integração com a API

---

## 🧪 Testes

- **Testes Unitários:** validações de horário e permissões
- **Testes de Integração:** login, registros, aprovações

---

## 📐 Diagramas

- **Modelo ER:** Usuário, Frequência, Atividade, Projeto
- **Casos de Uso:** registrar frequência, registrar atividade, aprovar registros

---

## 📌 Status do Projeto

> 🔄 Em desenvolvimento — funcionalidades sendo implementadas em sprints.

---

## 📊 Gerar o relatório de cobertura (excluindo Exceptions e Communication)
> dotnet tool run reportgenerator -reports:"**/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html  -classfilters:"-PontoEstagio.Exceptions*;-PontoEstagio.Communication*"