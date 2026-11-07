[Setup]
AppId={{A6B10A2E-FA20-4C8A-AC5D-7B3B9F2C4011}
AppName=ULTRAKILL: Pizza Mayhem
AppVersion=0.1.0
AppPublisher=mellizospf-dev
DefaultDirName={autopf}\Steam\steamapps\common\ULTRAKILL
DisableDirPage=yes
DirExistsWarning=no
OutputDir=..\dist
OutputBaseFilename=PizzaMayhem-Setup
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=lowest
Uninstallable=yes
WizardStyle=modern
CloseApplications=no

[Files]
Source: "..\dist\PizzaMayhem\PizzaMayhem.dll"; DestDir: "{code:GetGameDir}\BepInEx\plugins\PizzaMayhem"; Flags: ignoreversion

[Code]
var
  GameDirPage: TInputDirWizardPage;

function GetGameDir(Param: String): String;
begin
  if GameDirPage <> nil then
    Result := GameDirPage.Values[0]
  else
    Result := ExpandConstant('{autopf}\Steam\steamapps\common\ULTRAKILL');
end;

procedure InitializeWizard;
begin
  GameDirPage := CreateInputDirPage(wpWelcome,
    'Select ULTRAKILL folder',
    'Choose the folder containing ULTRAKILL.exe',
    'The installer places Pizza Mayhem in BepInEx\plugins and does not modify original game files.',
    False, '');
  GameDirPage.Add('ULTRAKILL installation folder:');
  GameDirPage.Values[0] := ExpandConstant('{autopf}\Steam\steamapps\common\ULTRAKILL');
end;

function NextButtonClick(CurPageID: Integer): Boolean;
var
  GameDir: String;
begin
  Result := True;
  if (GameDirPage <> nil) and (CurPageID = GameDirPage.ID) then
  begin
    GameDir := GameDirPage.Values[0];
    if not FileExists(GameDir + '\ULTRAKILL.exe') then
    begin
      MsgBox('ULTRAKILL.exe was not found. Select the correct game folder.', mbError, MB_OK);
      Result := False;
    end
    else if not DirExists(GameDir + '\BepInEx\plugins') then
    begin
      MsgBox('BepInEx was not found. Install BepInEx 5 and launch ULTRAKILL once before installing Pizza Mayhem.', mbError, MB_OK);
      Result := False;
    end;
  end;
end;
