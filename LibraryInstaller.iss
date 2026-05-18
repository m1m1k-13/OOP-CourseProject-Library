#define MyAppName "Программа управления ИС Библиотека"
#define MyAppVersion "1.0"
#define MyAppExeName "Library_CourseProject_Anikin_24VP2.exe"
#define MyAppPublisher "Аникин А.А. 24ВП2"

[Setup]
AppId={{8A3B2C1D-5F4E-4A6B-9C12-ABCDEF123456}}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\{#MyAppName}
DefaultGroupName={#MyAppName}
OutputDir=output
OutputBaseFilename=LibraryInstaller
Compression=lzma
SolidCompression=yes
WizardStyle=modern
SetupIconFile=icon.ico

[Languages]
Name: "russian"; MessagesFile: "compiler:Languages\Russian.isl"

[Tasks]
Name: "desktopicon"; Description: "Создать ярлык на рабочем столе"; GroupDescription: "Дополнительно:"; Flags: unchecked

[Files]
Source: "Library_CourseProject_Anikin_24VP2\bin\Release\net10.0-windows\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\Программа управления ИС Библиотека"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\Удалить программу"; Filename: "{uninstallexe}"
Name: "{commondesktop}\Программа управления ИС Библиотека"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Запустить программу"; Flags: nowait postinstall skipifsilent