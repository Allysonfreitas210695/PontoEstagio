# 📘 Registra

**Registra** é um sistema web desenvolvido para gerenciar o processo de solicitação, acompanhamento e validação de estágios supervisionados obrigatórios no ensino superior. A plataforma automatiza etapas burocráticas, organiza documentos essenciais e centraliza o controle das atividades realizadas pelos estagiários. O sistema permite que alunos, supervisores, representantes legais e coordenadores interajam de forma transparente, buscando promover mais agilidade e conformidade institucional.

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
- [] Upload de **comprovantes** ou anexos
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
- PostgreSQL
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

- **Diagrama de classe**
- **Diagrama de lógica**
- **Casos de Uso**

---

## 📌 Status do Projeto

> 🔄 Em desenvolvimento — funcionalidades sendo implementadas em sprints.

---

## 📊 Gerar o relatório de cobertura (excluindo Exceptions e Communication)

> dotnet tool run reportgenerator -reports:"\*_/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html -classfilters:"-PontoEstagio.Exceptions_;-PontoEstagio.Communication\*"
