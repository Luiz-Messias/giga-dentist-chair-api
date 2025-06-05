
# 🦷 Projeto Giga Consult API

API RESTful desenvolvida em .NET 8 para gerenciamento de cadeiras odontológicas.  
Implementa um sistema de alocação **rotacional e proporcional** de cadeiras entre dentistas.

---

## 📚 Objetivo do Teste

Desenvolver uma API de gerenciamento de cadeiras de dentista utilizando a plataforma .NET.  
O aplicativo deve permitir a criação, leitura, atualização e exclusão de cadeiras.

---

## 📋 Requisitos Funcionais

1. Cadastro de novas cadeiras, com informações como número, descrição e status.
2. Visualização de todas as cadeiras cadastradas.
3. Atualização das informações de uma cadeira existente.
4. Exclusão de cadeiras.
5. Alocação automática de cadeiras com base em data/hora inicial e final:
   - **Rotaciona** o uso das cadeiras.
   - **Distribui proporcionalmente** as alocações entre as cadeiras de acordo com os horários definidos.

---

## 🛠️ Requisitos Técnicos

- Plataforma .NET (utilizado .NET 8)
- Banco de dados: **MySQL** (`gigadb`)
- Sem uso de ORM (como Entity Framework)
- Boas práticas aplicadas:
  - Separação de responsabilidades
  - Validação
  - Inversão de dependência com interfaces
  - Organização por camadas: Domain, Application, Infra, API

---

## ⚙️ Estrutura do Projeto

- `Domain`: Interfaces e entidades
- `Application`: Regras de negócio
- `Infra`: Repositórios com ADO.NET
- `API`: Controllers, Program.cs e configurações

---

## 🚀 Como Executar

### 🔧 Requisitos

- .NET 8 SDK
- MySQL instalado
- Visual Studio ou VS Code
- Navegador para acessar o frontend

### 🗃️ Configuração do Banco de Dados

1. Execute o script abaixo no MySQL para criar o banco e as tabelas necessárias:

```sql
-- Cria o banco de dados apenas se não existir
CREATE DATABASE IF NOT EXISTS gigadb;
USE gigadb;

-- Cria a tabela DentistChairs se não existir
CREATE TABLE IF NOT EXISTS DentistChairs (
    Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    Number INT NOT NULL,
    Description VARCHAR(255) NOT NULL,
    IsActive BOOLEAN NOT NULL
);

-- Cria a tabela Allocations se não existir
CREATE TABLE IF NOT EXISTS Allocations (
    Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    ChairId INT NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
    FOREIGN KEY (ChairId) REFERENCES DentistChairs(Id)
);

-- Índices para melhorar performance das consultas
CREATE INDEX idx_allocations_chairid ON Allocations(ChairId);
CREATE INDEX idx_allocations_starttime ON Allocations(StartTime);
CREATE INDEX idx_allocations_endtime ON Allocations(EndTime);
CREATE INDEX idx_chairs_isactive ON DentistChairs(IsActive);


3. Atualize a connection string nos repositórios:
```

```csharp
private readonly string _connectionString = "Server=localhost;Port=3306;Database=gigadb;User=root;Password=senha;";
```

> Ajuste `localhost`, `User` e `Password` conforme seu ambiente (ex: `127.0.0.1`).

### ▶️ Executar API

```bash
dotnet run --project src/ProjetoGiga.Api
```

- Acesse via:
  - `https://localhost:5001/swagger`
  - `http://localhost:5000/swagger`

---

## 💻 Executar Frontend

1. Vá até a pasta com `index.html`.
2. Clique duas vezes para abrir no navegador.
3. O frontend utiliza `fetch` para se comunicar com a API.

---

## 🧪 Critérios Atendidos

- ✅ CRUD completo de cadeiras
- ✅ Alocação rotacional e proporcional por horários
- ✅ Separação de responsabilidades
- ✅ Uso de boas práticas (sem frameworks externos)
- ✅ API documentada via Swagger
- ✅ Frontend funcional básico

---

## 👤 Autor

Luiz  
[LinkedIn](https://www.linkedin.com/in/luiz-messias)  

---

## 📄 Licença

Este projeto é para fins de avaliação técnica.