# ORK Installer (público)

Instalador del host nativo de **ORK - Registry Jumper**, configurado para la extensión **publicada**
en la Chrome Web Store, cuyo ID es:

```
chrome-extension://nipiloljmfhmfbaliciokjeakghfggkj/
```

- `com.microsoft.ork.json` — manifest del host nativo apuntando al ID de la extensión pública.
- `install_script.iss` — script Inno Setup 6.7.0 para empaquetar el host en `C:\Program Files\ORK`.

> La rama `pre-release-testing` mantiene una variante con el ID de la extensión desempaquetada local
> (`lallfogojflmbimobkadfhhaajiiakin`) solo para desarrollo. No publicar ese instalador.

## Cómo instalar

1. Descargá `ORK_Setup.exe` desde la release (o desde Actions → última corrida → artifact `ORK_Setup`).
2. Ejecutalo (instalación en silencio: `ORK_Setup.exe /VERYSILENT /SUPPRESSMSGBOXES /NORESTART`).
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

## Seguridad / trazabilidad del binario

La release se genera desde GitHub Actions (workflow `build-installer.yml`): el binario publicado es
el artefacto de la corrida disparada por el tag de la release. Eso permite verificar el origen:
**commit → workflow run → binario**. Cualquier binario subido a mano a una release NO debe
considerarse oficial; el oficial es siempre el artefacto de Actions del tag.