# ORK Installer (pre-release/testing)

Esta rama **`pre-release-testing`** contiene el instalador del host nativo configurado para la
extensión **desempaquetada** (pruebas locales), cuyo ID es:

```
chrome-extension://lallfogojflmbimobkadfhhaajiiakin/
```

- `com.microsoft.ork.json` — manifest del host nativo apuntando al ID de extensión local de testing.
- `install_script.iss` — script Inno Setup 6.7.0 para empaquetar el host en `C:\Program Files\ORK`.

## Diferencia con la rama pública (`main`)

| Rama | allowed_origins | Uso |
|---|---|---|
| `main` | `nipiloljmfhmfbaliciokjeakghfggkj` | Extensión publicada en la store (instalador público) |
| `pre-release-testing` | `lallfogojflmbimobkadfhhaajiiakin` | Extensión desempaquetada en desarrollo |

> El ID de testing es **arbitrario y puede cambiar** si se vuelve a cargar la extensión. No subir
> este instalador como público.