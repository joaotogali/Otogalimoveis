# Otogalimoveis

Sistema de aluguel de imóveis.

## 🏗️ Estrutura do Projeto

- **Otogalimoveis.Domain**: Entidades e interfaces de domínio
- **Otogalimoveis.Infrastructure**: Implementação de acesso a dados (EF Core)
- **Otogalimoveis.Application**: Serviços, regras de negócio, DTOs e AutoMapper
- **Otogalimoveis.WebAPI**: API RESTful (Swagger, controllers)
- **Otogalimoveis.DesktopApp**: Aplicação WinForms (CRUD)

## 🚀 Como rodar o projeto

### 1. Pré-requisitos
- .NET 8 SDK
- SQL Server (local ou remoto)

### 2. Configuração do Banco
- Edite o arquivo `Otogalimoveis.WebAPI/appsettings.json` com sua connection string:
  ```json
  "DefaultConnection": "Server=localhost\\YourInstance;Database=OtogalimoveisDb;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=True"
  ```
- Crie o banco e as tabelas rodando:
  ```sh
  dotnet ef database update --project Otogalimoveis.Infrastructure --startup-project Otogalimoveis.WebAPI
  ```

### 3. Rodando a API
```sh
  dotnet run --project Otogalimoveis.WebAPI
```
Acesse o Swagger em: [http://localhost:5261/swagger](http://localhost:5261/swagger)

## 📚 Exemplos de uso

- **Endpoints principais:**
  - `/api/imoveisdto`
  - `/api/locatariosdto`
  - `/api/alugueis`
- **Testes via Swagger:** Crie, edite, liste e exclua imóveis, locatários e aluguéis.

## 🛠️ Tecnologias

- .NET 8
- Entity Framework Core
- SQL Server
- AutoMapper
- Swagger
- WinForms

## 💡 Dicas

- Sempre pare a aplicação antes de rodar comandos de migração ou update no banco.
- Se mudar o modelo, crie uma nova migração:
  ```sh
  dotnet ef migrations add NomeDaMigracao --project Otogalimoveis.Infrastructure --startup-project Otogalimoveis.WebAPI
  dotnet ef database update --project Otogalimoveis.Infrastructure --startup-project Otogalimoveis.WebAPI
  ```

---

**Dúvidas ou sugestões? Abra uma issue ou entre em contato!** 
