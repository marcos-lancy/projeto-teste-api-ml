# Projeto Teste Meli

### Princípios de Design
- **Domain-Driven Design (DDD)**: Entidades ricas com regras de negócio
- **Clean Architecture**: Separação clara de responsabilidades
- **SOLID**: Princípios aplicados em toda a codebase
- **RESTful**: Endpoints seguindo convenções REST

## 📊 Endpoints Principais

### Autenticação
- `POST /api/v1/auth/entrar` - Autenticação JWT

### Produtos (CRUD Completo)
- `GET /api/v1/produtos` - Listagem paginada
- `GET /api/v1/produtos/{id}` - Detalhes do produto
- `POST /api/v1/produtos` - Criar produto
- `PUT /api/v1/produtos/{id}` - Atualizar produto
- `DELETE /api/v1/produtos/{id}` - Remover produto

## Testes Unitários

Este projeto inclui testes unitários para garantir a qualidade do código e o funcionamento correto das funcionalidades.

- Os testes são escritos usando **xUnit**.
- Para gerar dados de teste, utilizamos **Bogus**.
- Para executar os testes:

```bash
dotnet test
```

## Bibliotecas Utilizadas

Este projeto faz uso das seguintes bibliotecas:

- **[FluentValidation](https://fluentvalidation.net/)**: Validação fluente de objetos e DTOs.
- **[Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)**: Geração automática de documentação Swagger para APIs ASP.NET Core.
- **[Bogus](https://github.com/bchavez/Bogus)**: Geração de dados falsos (mock) para testes.


## ⚙️ Estrutura do Projeto

```plaintext
Sol.TesteMeli/
├── src/
│   ├── TesteMeli.Api/        # Controllers e DTOs
│   ├── TesteMeli.Business/   # Domínio e regras de negócio
│   ├── TesteMeli.Data/       # Repositórios e persistência
│   ├── TesteMeli.Shared/     # Recursos compartilhados entre camadas
│   └── TesteMeli.Tests/      # Testes unitários/integração
├── infra/
│   └── docker-compose.yml    # Orquestração dos containers
├── .gitignore
├── LICENSE
└── README.md
```

## ⚙️ Instruções de Configuração

### Pré-requisitos
- .NET 9 SDK
- IDE (VS Code, Visual Studio ou Rider)

### Execução Rápida
```bash
# Clone o projeto
git clone https://github.com/marcos-lancy/projeto-teste-api-ml
cd TesteMeli

# Restaure dependências
dotnet restore

# Execute a API
dotnet run --project TesteMeli.Api

# Acesse a documentação
# https://localhost:44334/swagger
```

## 🚀 Como subir a aplicação com Docker Compose

### Pré-requisitos
- Docker e Docker Compose instalados (ou Docker Desktop).  
- Estar na raiz do repositório (`Sol.TesteMeli/`) no seu terminal.

### Passo a passo

1. Abra o terminal na raiz do projeto:
   ```bash
   cd projeto-teste-api-ml/infra
   ```

2. Subir os containers (com build):
   ```bash
   docker compose -f docker-compose.yml up --build -d
   ```

3. Acesse a aplicação:
   - Abra `http://localhost:<porta_host>` (ex.: `http://localhost:8080/swagger`).


## 🛠️ Troubleshooting

- **Porta ocupada** → altere a porta host no `docker-compose.yml`.
- **Build falhando** → verifique o `Dockerfile` no contexto configurado.
- **Container reiniciando** → cheque os logs:
