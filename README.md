# Contoso Bookstore Template

> **Repo template** del proyecto integrador opcional del curso AZ-400T00-A (cohorte EducIT 81958, jul-sep 2026).
> Sirve como base para que los alumnos apliquen las prácticas DevOps que se enseñan en clase a un proyecto real.

## Stack

- **.NET 10** Web API (controllers) con EF Core
- **DB:** Azure SQL (en producción) · SQL Server Edge (en docker-compose) · InMemory (default si no hay connection string, ideal para `dotnet run` directo)
- **Front:** vanilla HTML + Bootstrap 5 (servido desde `wwwroot/`) — sin npm, sin build
- **Containerización:** Dockerfile multi-stage + docker-compose con SQL Edge
- **OpenAPI:** integrado nativo de .NET 10 (`/openapi/v1.json`)
- **Tests:** xUnit + `WebApplicationFactory<Program>`

## Estructura

```
.
├── src/
│   ├── ContosoBookstore.Api/         # Web API + frontend en wwwroot/
│   │   ├── Controllers/              # Books, Authors, Health, Config
│   │   ├── Data/                     # DbContext + SeedData
│   │   ├── Models/                   # Book, Author
│   │   └── wwwroot/index.html        # UI Bootstrap + fetch()
│   └── ContosoBookstore.Common/      # DTOs compartidos (futuro NuGet en Fase 7)
├── tests/
│   └── ContosoBookstore.Api.Tests/   # Integration tests con WebApplicationFactory
├── infra/                            # Bicep templates (placeholder, Fase 5)
├── .github/                          # CI workflows, CODEOWNERS, templates
├── Dockerfile
├── docker-compose.yml
└── STUDENTS.md                       # ⬅️ Las 10 fases del integrador
```

## Quick start

### Opción 1: dotnet run directo (InMemory DB, sin docker)

```bash
cd src/ContosoBookstore.Api
dotnet run
```

Abrir http://localhost:5xxx (la API te muestra el puerto). El frontend Bootstrap está en la raíz; el API en `/api/books`, `/api/authors`, `/api/health`, `/api/config`; OpenAPI en `/openapi/v1.json`.

### Opción 2: docker-compose (con SQL Server real)

```bash
docker compose up --build
```

Abrir http://localhost:8080.

## Testing

```bash
dotnet test
```

4 tests integración via `WebApplicationFactory<Program>`. Todos pasan al checkout-ear el repo.

## Para alumnos del curso AZ-400

Si estás cursando AZ-400 con Educación IT y querés hacer el proyecto integrador opcional:

1. Hacé fork (o usá "Use this template") en GitHub
2. Cloná tu fork localmente
3. Verificá que compila con `dotnet build` y los tests pasan con `dotnet test`
4. Seguí las 10 fases definidas en [STUDENTS.md](STUDENTS.md), una por clase aprox.
5. Demo final en C12 (opcional, ~8-10 min)

## Licencia

Este código es material de capacitación. Sentite libre de usarlo como base para tus propios proyectos.
