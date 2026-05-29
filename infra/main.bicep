// TODO (Fase 5 del integrador): este es un placeholder vacío.
// Los alumnos lo completan en post-C7 con:
//   - App Service Plan + App Service
//   - Azure SQL Server + Database
//   - Key Vault
//   - Application Insights + Log Analytics
//   - Modularización: separar en module-appservice.bicep, module-sql.bicep, etc.
//   - Parameters por environment (dev.bicepparam, staging.bicepparam, prod.bicepparam)
//   - Tags policy (Environment, Project, Owner)
//
// Ver guía en STUDENTS.md → Fase 5.

@description('Location for all resources')
param location string = resourceGroup().location

@description('Environment name (dev, staging, prod)')
@allowed([
  'dev'
  'staging'
  'prod'
])
param environmentName string = 'dev'

@description('Project name prefix')
param projectName string = 'contoso-bookstore'

// Recursos van acá. Por ahora solo placeholders comentados:
//
// resource appServicePlan 'Microsoft.Web/serverfarms@2024-04-01' = { ... }
// resource appService 'Microsoft.Web/sites@2024-04-01' = { ... }
// resource sqlServer 'Microsoft.Sql/servers@2024-05-01-preview' = { ... }
// etc.

output environment string = environmentName
output projectName string = projectName
