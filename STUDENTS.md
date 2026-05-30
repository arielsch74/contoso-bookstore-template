# Proyecto integrador · Las 10 fases · Contoso Bookstore

> Guía para alumnos del curso AZ-400T00-A (EducIT 81958) que decidan hacer el proyecto integrador opcional.

## Cómo funciona

1. **NO hagas fork.** Click en el botón verde **"Use this template"** en la página del repo → "Create a new repository". Eso te crea una copia LIMPIA del template en tu cuenta, sin relación upstream con el original (más prolijo para portfolio).
2. Cada clase del curso aborda conceptos que después aplicás a tu copia.
3. Vas completando fases entre clases (1 fase por clase aprox).
4. En C11 (lab integrador) pulís y preparás demo.
5. En C12 (opcional) demostrás tu trabajo en 8-10 min.

**No es obligatorio.** El curso pasa bien sin esto. Pero si lo terminás, salís con un portfolio piece real para mostrar en entrevistas DevOps.

> 💡 **¿Por qué "Use this template" y no "fork"?** Un fork queda permanentemente linkeado al repo original como upstream — útil para contribuir, ruidoso para portfolio. Un template-based repo es independiente: no aparece "forked from arielsch74/..." en tu repo, queda como proyecto tuyo limpio.

---

## Fase 1 — Source control + Git workflows (post C2)

**Objetivo:** repo configurado profesionalmente.

- [ ] Branching strategy implementada (recomendado: GitHub Flow simplificado)
- [ ] Branch protection en `main`: require PR + 1 reviewer + status checks + linear history
- [ ] CODEOWNERS file (apuntando a tu usuario)
- [ ] Issue templates (bug / feature / question) — ya hay placeholders en `.github/ISSUE_TEMPLATE/`
- [ ] PR template con checklist — ya hay placeholder
- [ ] README con badges (build status, license, version)
- [ ] CONTRIBUTING.md (Inner Source ready)
- [ ] GitHub Project v2 con 5-7 work items iniciales

## Fase 2 — Pipelines CI (post C4)

**Objetivo:** integración continua funcionando en GitHub Actions Y Azure Pipelines.

- [ ] Workflow GitHub Actions `ci.yml`: triggers `push` + `pull_request`; jobs restore/build/test/publish-artifact; matrix .NET 8 LTS en ubuntu + windows (usar `dotnet-version: '8.0.x'` en actions/setup-dotnet@v4)
- [ ] Pipeline equivalente en Azure DevOps YAML (`azure-pipelines.yml`)
- [ ] Docker build + push a GitHub Container Registry (ghcr.io)
- [ ] Status check del workflow CI configurado en branch protection

## Fase 3 — Release pipeline (post C5)

**Objetivo:** deploy automatizado con stages.

- [ ] Workflow CD `cd.yml` con 3 environments: dev → staging → prod
- [ ] Environment `prod` con required reviewers
- [ ] Variable groups linked a Key Vault para connection strings
- [ ] Pre-deployment gate: build status passing del CI
- [ ] Post-deployment: smoke test (HTTP request a `/api/health`)

## Fase 4 — Secure CD + WIF + App Configuration (post C6)

**Objetivo:** sin secrets en pipelines.

> 📖 **Si nunca tocaste Entra ID o WIF:** leé primero el **[WIF Walkthrough](https://github.com/arielsch74/az400-educacionit-81958/blob/main/WIF-WALKTHROUGH.md)** del repo de prep del curso. Cubre la teoría (qué es Entra ID, App Registration, Federated Credential, OIDC), los comandos Azure CLI paso a paso, el workflow YAML mínimo viable, y los 4 errores típicos con cómo diagnosticarlos. ~15 min de lectura, te ahorra 2 horas de "look at this YAML and pray".

- [ ] Service principal con federated credential configurado en Entra ID
- [ ] Workflow CD usa `azure/login@v2` con `client-id` + `tenant-id` + `subscription-id` (sin client-secret)
- [ ] `permissions: id-token: write` en el workflow
- [ ] Azure App Configuration con feature flag "NewBookListing"
- [ ] El `ConfigController` ya está preparado para leer el flag — ahora apunta a App Configuration (en vez de appsettings local)
- [ ] Toggle del flag en App Config debería verse en el front sin redeploy

## Fase 5 — IaC con Bicep (post C7)

**Objetivo:** toda la infra declarativa.

- [ ] Bicep templates en `infra/`:
  - `main.bicep` (orquesta todo)
  - `module-appservice.bicep`
  - `module-sql.bicep`
  - `module-keyvault.bicep`
  - `module-appinsights.bicep`
- [ ] Parameters por environment (`dev.bicepparam`, `staging.bicepparam`, `prod.bicepparam`)
- [ ] Workflow `infra-deploy.yml` con `az deployment group create`
- [ ] `az deployment group what-if` step antes del deploy
- [ ] Tag policy via Bicep (Environment, Project, Owner)

## Fase 6 — Security scanning (post C8)

**Objetivo:** shift-left security activo.

- [ ] CodeQL workflow habilitado (autogenerado en Security tab)
- [ ] Secret scanning con push protection activado en repo settings
- [ ] `dependabot.yml` con nuget + docker + github-actions (weekly schedule)
- [ ] PR de Dependabot mergeado al menos una vez
- [ ] Step Mend Bolt **o** OWASP Dependency-Check en CI
- [ ] Microsoft Defender for Cloud habilitado (free tier)
- [ ] Azure Policy "Audit App Services without HTTPS only" assignado
- [ ] App Service forzado a HTTPS only via Bicep

## Fase 7 — Dependency management (post C9)

**Objetivo:** package management end-to-end.

- [ ] El proyecto `ContosoBookstore.Common` ya está separado — ahora se publica como NuGet
- [ ] Publish a GitHub Packages desde workflow `package-publish.yml`
- [ ] Bumpear versión con semver al hacer release tag
- [ ] Proyecto principal consume el NuGet desde GitHub Packages (con auth PAT)
- [ ] Bonus: misma library también a Azure Artifacts feed (para comparar)

## Fase 8 — Continuous feedback (post C10)

**Objetivo:** observabilidad y feedback loops.

- [ ] OpenTelemetry SDK integrado en la API
- [ ] Application Insights recibiendo telemetría en vivo
- [ ] Smart Detection habilitado
- [ ] Custom Workbook con: requests/min, error rate, latency p95, top failing endpoints
- [ ] Alert rule: error rate >5% en 5 min → action group
- [ ] Action group dispara Logic App que postea en MS Teams (o Discord)
- [ ] Wiki page del repo con diagrama Mermaid de la arquitectura

## Fase 9 — Integración en C11 (en clase, 3 hs)

- Pulís fases pendientes
- Refactorizás
- Preparás tu demo C12

## Fase 10 — Demo en C12 (opcional, 8-10 min)

Estructura sugerida:
1. **30 seg** — Stack overview
2. **1 min** — Demo flujo Git → PR → checks
3. **2 min** — Demo deploy a prod via WIF
4. **1 min** — App funcionando + feature flag toggle visible
5. **2 min** — App Insights workbook + alerta de prueba
6. **30 seg** — Diagrama Mermaid del wiki
7. **1 min** — Q&A

¡Mucha suerte!
