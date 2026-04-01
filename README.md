# 🚀 TechHigh Technologies - Employee & Department Management System

---

## 📌 Sobre o Projeto

O TechHigh Technologies é uma aplicação Fullstack desenvolvida com foco em boas práticas de arquitetura, separação de responsabilidades e simulação de um ambiente real de mercado utilizando microserviços.

O sistema permite o gerenciamento completo de:

- 👨💼 Funcionários (Employees)
- 🏢 Departamentos (Departments)

Com regras de negócio bem definidas, comunicação entre serviços e uma interface moderna construída em React.

---

## 🧠 Arquitetura do Projeto

O projeto foi estruturado seguindo princípios de Clean Architecture e separação por camadas, além de simular uma arquitetura baseada em microserviços.

---

## 🔹 Estrutura Backend

O backend é dividido em dois serviços independentes:

### 1️⃣ Employee API

Responsável por:

- Cadastro de funcionários
- Atualização de dados
- Exclusão (soft delete)
- Comunicação com o serviço de departamentos

---

### 2️⃣ Department API

Responsável por:

- Cadastro de departamentos
- Controle da quantidade de funcionários
- Regras de integridade (ex: não permitir exclusão com funcionários vinculados)

---

## 🔗 Comunicação entre Serviços

A comunicação entre os serviços é feita via HTTP (REST) utilizando um client dedicado:

- IDepartmentClient
- DepartmentClient

📌 Exemplo de implementação

```csharp
public interface IDepartmentClient
{
    Task Increment(Guid departmentId);
    Task Decrement(Guid departmentId);
    Task<DepartmentDto> GetById(Guid id);
}

public class DepartmentClient : IDepartmentClient
{
    private readonly HttpClient _http;

    public async Task Increment(Guid departmentId)
    {
        await _http.PostAsync($"/api/department/{departmentId}/increment", null);
    }
}
```

---

## 🔁 Exemplos de integração

- ➕ Ao criar um funcionário → acrescenta automaticamente na quantidade de funcionários do departamento.
- ➖ Ao deletar um funcionário → remove automaticamente da quantidade de funcionários do departamento.
- 🔍 Consulta de departamento durante operações

---

## 🔄 Fluxo de Funcionamento (Exemplo Real)

### ➕ Criação de um Funcionário

1. O frontend envia um POST /api/employee
2. O Controller recebe os dados
3. O Service executa a regra de negócio
4. O Service chama o DepartmentClient
5. O Department API incrementa o contador
6. O Employee é salvo no banco

---

### 🗑️ Exclusão de Funcionário

1. O Service verifica se existe
2. Realiza um soft delete
3. Chama o Department API
4. Decrementa o número de funcionários

---

### 🏢 Exclusão de Departamento

```csharp
if(department?.AmountEmployee > 0) 
    throw new Exception("impossible to delete, there are employees");
```

👉 Regra de negócio crítica garantindo integridade dos dados.

---

## 🧱 Camadas da Aplicação

### 📁 Domain

Contém:

- Entidades (Employee, Department)
- Regras de negócio
- Validações no construtor

📌 Exemplo de entidade com regra

```csharp
public void SetAdmissionDate(DateOnly date)
{
    if(date > DateOnly.FromDateTime(DateTime.Now))
        throw new Exception("Invalid date");

    AdmissionDate = date;
}
```

👉 Aqui você garante que a regra de negócio não dependa do frontend.

---

### 📁 Application / Service

Responsável por:

- Regras de negócio
- Orquestração das operações
- Comunicação entre serviços

📌 Exemplo de método (Service)

```csharp
public async Task Delete(Guid id)
{
    var employee = await _repository.FindById(id);

    if(employee == null)
        throw new AppException("Employee not found");

    await _departmentClient.Decrement(employee.DepartmentId);

    await _repository.Delete(id);
}
```

📌 Exemplo adicional (Create)

```csharp
public async Task Create(EmployeeDto dto)
{
    var employee = new Employee(dto.Name, dto.Email, dto.Salary);

    await _repository.Add(employee);

    await _departmentClient.Increment(dto.DepartmentId);
}
```

---

### 📁 Infrastructure

Contém:

- Repositórios
- Integração com banco de dados (Entity Framework Core)
- Clients HTTP

📌 Exemplo de Repository

```csharp
public async Task<Employee> FindById(Guid id)
{
    return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
}
```

---

### 📁 API (Controllers)

Responsável por:

- Exposição dos endpoints REST
- Recebimento e retorno de dados (DTOs)

📌 Exemplo de Controller

```csharp
[HttpPost]
public async Task<IActionResult> Create(EmployeeDto dto)
{
    await _service.Create(dto);
    return Ok();
}
```

---

## ⚙️ Tecnologias Utilizadas

### 🔧 Backend

- C#
- .NET Core
- Entity Framework Core
- SQLite
- REST API
- HttpClient

### 🎨 Frontend

- React
- React Router DOM
- JavaScript (ES6+)
- Bootstrap
- CSS

---

## 🗄️ Banco de Dados

O projeto utiliza:

- 🟢 SQLite

✅ Vantagens

- Leve e simples de configurar
- Ideal para projetos de portfólio
- Não requer instalação de servidor

---

## 🔥 Funcionalidades

### 👨💼 Funcionários

- ✅ Cadastro de funcionário
- ✏️ Edição
- 🗑️ Exclusão (soft delete)
- 🔍 Filtro e busca
- 📅 Controle de data de admissão
- 💰 Controle de salário
- 🔗 Associação com departamento

---

### 🏢 Departamentos

- ✅ Cadastro
- ✏️ Edição
- 🗑️ Exclusão com regra de validação
- 📊 Controle automático de quantidade de funcionários

---

## 🧠 Regras de Negócio

- ❌ Não é possível deletar departamento com funcionários
- 🔄 Contador de funcionários atualizado automaticamente
- ✔️ Validações no backend e frontend
- ⚠️ Tratamento de exceções customizadas

---

## ⚠️ Tratamento de Erros

O sistema possui:

- Exceptions customizadas (AppException)
- Middleware global para tratamento de erros
- Retorno padronizado para o frontend

---

## 🎨 Frontend

O frontend foi desenvolvido como uma SPA (Single Page Application).

📌 Funcionalidades

- 🔄 Navegação com React Router
- 📡 Consumo de API
- 📝 Formulários controlados
- 🔍 Busca e filtros
- ✏️ Edição dinâmica
- 🗑️ Exclusão com confirmação
- 📊 Listagem em tabela
- 📅 Formatação de datas
- 💰 Formatação de valores (pt-BR)

---

## 🧠 Lógica do Frontend (Exemplo)

📌 Envio de formulário

```javascript
const handleSubmit = async (e) => {
  e.preventDefault();

  const response = await fetch(API_URL, {
    method: "POST",
    headers: {"Content-Type": "application/json"},
    body: JSON.stringify(employee)
  });
};
```

📌 Validação de formulário

```javascript
const isInvalid =
  !name ||
  name.trim() === "" ||
  !email ||
  salary <= 0;
```

---

## 💡 Diferenciais do Projeto

- 🧩 Arquitetura baseada em microserviços
- 🔗 Comunicação entre APIs
- 🧠 Separação clara de responsabilidades
- ⚠️ Tratamento de erros profissional
- 🗄️ Uso de ORM (Entity Framework)
- 📡 Integração real entre sistemas
- ⚛️ Frontend moderno e interativo

---

## 🚀 Como Executar o Projeto

### 🔧 Backend

```bash
git clone https://github.com/seu-usuario/seu-repo.git
dotnet run
```

### 💻 Frontend

```bash
npm install
npm start
```

---

## 📡 Endpoints Principais

### 👨💼 Employee API

- GET /api/employee
- POST /api/employee
- PUT /api/employee/{id}
- DELETE /api/employee/{id}

### 🏢 Department API

- GET /api/department
- POST /api/department
- PUT /api/department/{id}
- DELETE /api/department/{id}

---

## 💼 Sobre o Projeto (Portfólio)

Este projeto foi desenvolvido com foco em demonstrar:

- Conhecimento em .NET + React
- Aplicação de boas práticas de arquitetura
- Experiência com APIs REST
- Integração entre serviços
- Construção de aplicações fullstack completas

---

## ⭐ Conclusão

Este projeto representa uma aplicação completa, cobrindo backend e frontend, simulando cenários reais do mercado com foco em arquitetura e boas práticas.

---

## 📚 Documentação Técnica Avançada (EXTRA)

### 🔍 Métodos Importantes - Backend

#### ➕ Create Employee

- Cria entidade
- Aplica regra de domínio
- Atualiza outro microserviço
- Persiste no banco

#### ✏️ Update Employee

- Busca entidade existente
- Valida existência
- Atualiza propriedades
- Salva no banco

#### 🗑️ Delete Employee

- Soft delete
- Atualiza departamento
- Mantém consistência

---

## 🔗 Integração entre APIs

- Uso de HttpClient
- Tratamento de erro externo
- Comunicação síncrona

---

## 🎨 Métodos Frontend

### handleSubmit

- Decide entre POST / PUT
- Envia JSON
- Atualiza UI

### useEffect

- Executa ao carregar
- Busca dados automaticamente

### useState

- Controle de estado dos inputs
- Sincronização com UI

---

## 🧠 Decisões Técnicas

- Separação em microserviços
- Uso de DTOs
- Repository Pattern
- Service Layer
- Validações no domínio

---

## 📸 Screenshots

![1](./images/1.png)

---

![2](./images/2.png)

---

![3](./images/3.png)

---

![4](./images/4.png)

---

![5](./images/5.png)

---

![6](./images/6.png)

---

![7](./images/7.png)

---

![8](./images/8.png)

---

![9](./images/9.png)

---

![10](./images/10.png)

---

![11](./images/11.png)

---

![12](./images/12.png)

---

![13](./images/13.png)

---

![14](./images/14.png)

---

![15](./images/15.png)

---

![16](./images/16.png)

---

![17](./images/17.png)

---

![18](./images/18.png)

---

![19](./images/19.png)

---

![20](./images/20.png)

---

![21](./images/21.png)

---

![22](./images/22.png)

---

![23](./images/23.png)

---

![24](./images/24.png)

---

![25](./images/25.png)

---

![26](./images/26.png)

---

![27](./images/27.png)

---

![28](./images/28.png)

---

![29](./images/29.png)

---

![30](./images/30.png)

---

![31](./images/31.png)

---

![32](./images/32.png)

---

![33](./images/33.png)

---

![34](./images/34.png)

---

![35](./images/35.png)

---

![36](./images/36.png)

---

![37](./images/37.png)

---

![38](./images/38.png)

---

![39](./images/39.png)

---

![40](./images/40.png)

---

![41](./images/41.png)

---

![42](./images/42.png)

---

![43](./images/43.png)

---

![44](./images/44.png)

---

![45](./images/45.png)

---

![46](./images/46.png)
