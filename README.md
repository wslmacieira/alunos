
# Alunos - Web API com PostgresSQL e Docker .NET Core

## Como executar a api
```
#faça o clone do repositorio
git clone https://github.com/wslmacieira/alunos.git

# necessario ter o docker e docker compose instalados
# Cria a image docker
docker build -t alunoswebapi .

# fazer o build
docker-compose build

# Subir o container com docker compose
docker-compose up

# Executar as migrations para criar a tabela
dotnet ef database update   

# Acessar a aplicação
http://localhost:8000/swagger/index.html
# Ou
http://localhost:32033/swagger/index.html

# Limpar a base de dados
dotnet ef database drop
# depois
dotnet ef database update
```

