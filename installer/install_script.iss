; ORK Native Host - pre-release/testing installer (local extension ID)
; Reconstructed from ORK_Setup.exe (Inno Setup 6.7.0)
[Setup]
AppName=ORK Native Host
AppId={{B2977C70-5F33-4138-9D76-CAFE35924825}}
AppVersion=1.0.0
AppPublisher=Miguel Okstein
DefaultDirName={commonpf}\ORK
UsePreviousAppDir=no
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
Source: "..\publish\Ork.deps.json"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\publish\Ork.dll"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\publish\Ork.exe"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\publish\Ork.pdb"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\publish\Ork.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion 
Source: "..\publish\ork-icon.png"; DestDir: "{app}"; Flags: ignoreversion 

[Registry]
Root: HKCU; Subkey: "Software\Google\Chrome\NativeMessagingHosts\com.microsoft.ork"; ValueType: String; ValueData: "{app}\com.microsoft.ork.json"; Flags: uninsdeletekey 
Root: HKCU; Subkey: "Software\Microsoft\Edge\NativeMessagingHosts\com.microsoft.ork"; ValueType: String; ValueData: "{app}\com.microsoft.ork.json"; Flags: uninsdeletekey 

[Icons]
; Shortcut con AUMID: requerido para que los toasts de apps win32 no empaquetadas se muestren
Name: "{autoprograms}\ORK Registry Jumper"; Filename: "{app}\Ork.exe"; WorkingDir: "{app}"; IconFilename: "{app}\ork-icon.png"; AppUserModelID: "ORK.NativeHost"; Flags: createonlyiffileexists 

[UninstallDelete]
Type: dirifempty; Name: "{app}"; 

[Languages]
Name: "default"; MessagesFile: "compiler:Default.isl";