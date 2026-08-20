; ORK Native Host - pre-release/testing installer (local extension ID)
; Reconstructed from ORK_Setup.exe (Inno Setup 6.7.0)
[Setup]
AppName=ORK Native Host
AppId={{B2977C70-5F33-4138-9D76-CAFE35924825}}
AppVersion=1.0.0
AppPublisher=Miguel Okstein
DefaultDirName={commonpf}\ORK
OutputBaseFilename=ORK_Setup
Compression=lzma2
ArchitecturesAllowed=x64compatible
ArchitecturesInstallIn64BitMode=x64compatible
DisableDirPage=yes
DisableProgramGroupPage=auto
ChangesAssociations=no
ShowLanguageDialog=yes
WizardStyle=classic light

[Files]
Source: "com.microsoft.ork.json"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\native-host\bin\Release\net8.0-windows10.0.17763.0\win-x64\publish\Ork.deps.json"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\native-host\bin\Release\net8.0-windows10.0.17763.0\win-x64\publish\Ork.dll"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\native-host\bin\Release\net8.0-windows10.0.17763.0\win-x64\publish\Ork.exe"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\native-host\bin\Release\net8.0-windows10.0.17763.0\win-x64\publish\Ork.pdb"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\native-host\bin\Release\net8.0-windows10.0.17763.0\win-x64\publish\Ork.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\native-host\bin\Release\net8.0-windows10.0.17763.0\win-x64\publish\ork-icon.png"; DestDir: "{app}"; Flags: ignoreversion 

[Registry]
Root: HKCU; Subkey: "Software\Google\Chrome\NativeMessagingHosts\com.microsoft.ork"; ValueType: String; ValueData: "{app}\com.microsoft.ork.json"; Flags: uninsdeletekey 
Root: HKCU; Subkey: "Software\Microsoft\Edge\NativeMessagingHosts\com.microsoft.ork"; ValueType: String; ValueData: "{app}\com.microsoft.ork.json"; Flags: uninsdeletekey 

[UninstallDelete]
Type: dirifempty; Name: "{app}"; 

[Languages]
Name: "default"; MessagesFile: "compiler:Default.isl";