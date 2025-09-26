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