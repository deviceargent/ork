$ErrorActionPreference = 'Stop'

$HostName = 'com.microsoft.ork'
$ExtensionId = 'lallfogojflmbimobkadfhhaajiiakin'
$InstallDir = Join-Path $env:LOCALAPPDATA 'ORK-Test-NativeHost'
$ExeSource = Join-Path $PSScriptRoot 'RegOpenerHost.exe'
$ExeTarget = Join-Path $InstallDir 'RegOpenerHost.exe'
$ManifestPath = Join-Path $InstallDir "$HostName.json"

if (-not (Test-Path $ExeSource)) {
    throw "RegOpenerHost.exe was not found next to this script."
}

New-Item -ItemType Directory -Force -Path $InstallDir | Out-Null
Copy-Item $ExeSource $ExeTarget -Force

$Manifest = @{
    name = $HostName
    description = 'ORK temporary Native Messaging Host for unpacked-extension testing'
    path = $ExeTarget
    type = 'stdio'
    allowed_origins = @("chrome-extension://$ExtensionId/")
} | ConvertTo-Json -Depth 3

Set-Content -Path $ManifestPath -Value $Manifest -Encoding UTF8

$RegistryPath = "HKCU:\Software\Microsoft\Edge\NativeMessagingHosts\$HostName"
New-Item -Path $RegistryPath -Force | Out-Null
Set-ItemProperty -Path $RegistryPath -Name '(Default)' -Value $ManifestPath

Write-Host "Installed temporary ORK Native Messaging Host."
Write-Host "Extension ID: $ExtensionId"
Write-Host "Host manifest: $ManifestPath"
Write-Host "Native host executable: $ExeTarget"
