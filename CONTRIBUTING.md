# Contributing

Este repo es un template educativo. Si encontrás un bug o querés sugerir mejoras, sentite libre de abrir un issue o PR.

## Inner Source guidelines

Este repo está organizado siguiendo prácticas de Inner Source (open-source-style collaboration adentro de una empresa). Esto significa:

- **Fork & PR flow:** todos los cambios entran via Pull Request, incluso del owner.
- **Code review obligatorio:** mínimo 1 reviewer aprobando antes de mergear.
- **Issues abiertas:** documentás el "por qué" antes de codear.
- **Documentación junto al código:** README + STUDENTS.md siempre actualizados.

## PR checklist

Antes de abrir un PR:

- [ ] Branch creada desde `main` actualizado
- [ ] Código compila localmente (`dotnet build`)
- [ ] Tests pasan localmente (`dotnet test`)
- [ ] Si agregás endpoints nuevos, sumá test de integración
- [ ] Si tocás el frontend, probaste manualmente en el browser
- [ ] Mensajes de commit descriptivos (no "fix" / "wip")
- [ ] PR vinculado a un issue si aplica (`Fixes #123`)
