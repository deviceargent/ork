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

## Cómo probar

1. Instalá el `ORK_Setup.exe` del artefacto del workflow (Actions → última corrida → artifact `ORK_Setup`).
2. Cargá la extensión desempaquetada (`extension/`) en Edge/Chrome.
3. Seleccioná un path de regedit (ej. `HKEY_CURRENT_USER\Software\...`) en cualquier página.
4. Clic derecho → **ORK - Registry Jumper** → se abre regedit y aparece el toast `ARGGGGGGG!`.

## Reinstalar / actualizar el host

Si el instalador corre pero **no reemplaza los archivos**, es porque `Ork.exe` (el host nativo)
está en ejecución y Edge lo sostiene con un handle del pipe de Native Messaging. Inno Setup
(RestartManager) no puede cerrarlo y en modo silencioso aborta la instalación.

Solución antes de reinstalar:

```powershell
# Cerrar el navegador y/o matar el host colgado
Get-Process -Name Ork -ErrorAction SilentlyContinue | Stop-Process -Force
```

Luego correr el setup normalmente (sí sobrescribe: todo `[Files]` usa `Flags: ignoreversion`).