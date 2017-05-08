;XBMC for Windows install script
;Copyright (C) 2005-2013 Team XBMC
;http://xbmc.org

;Script by chadoe
;Changed by DanCooper

;--------------------------------
;Include Modern UI

  !include "MUI2.nsh"
  !include "nsDialogs.nsh"
  !include "LogicLib.nsh"
  !include "WinVer.nsh"

;--------------------------------
;define global used name
  ;!define APP_NAME "Ember Media Manager"
  ;!define emm_root ".."
  ;!define emm_release "EmberMM - Release - x86"
  ;!define emm_revision "1.4.0.0"
  ;!define emm_branch "Beta"
  ;!define transifex_path ".\translations"

;--------------------------------
;General

  ;Name and file
  Name "${emm_appname}"
  OutFile "${emm_outfile}"

  XPStyle on

  ;Default installation folder
  ;InstallDir "$PROGRAMFILES\${APP_NAME}"
  InstallDir "C:\${emm_appname}"

  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "Software\${emm_appname}" ""

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin

;--------------------------------
;Variables

  Var StartMenuFolder
  ;Var PageSettingsState
  ;Var PageTempState
  ;Var DirectXSetupError
  ;Var VSRedistSetupError

;--------------------------------
;Interface Settings

  !define MUI_HEADERIMAGE
  ;!define MUI_ICON "..\..\xbmc\win32\xbmc.ico"
  ;!define MUI_WELCOMEFINISHPAGE_BITMAP "xbmc-left.bmp"
  !define MUI_COMPONENTSPAGE_SMALLDESC
  !define MUI_FINISHPAGE_LINK "Please visit http://embermediamanager.org for more information."
  !define MUI_FINISHPAGE_LINK_LOCATION "http://embermediamanager.org"
  ;!define MUI_FINISHPAGE_RUN "$INSTDIR\${emm_filename}"
  ;!define MUI_FINISHPAGE_RUN_NOTCHECKED
  !define MUI_ABORTWARNING
;--------------------------------
;Pages

  !insertmacro MUI_PAGE_WELCOME
  ;!insertmacro MUI_PAGE_LICENSE "..\..\LICENSE.GPL"
  !insertmacro MUI_PAGE_COMPONENTS
  !insertmacro MUI_PAGE_DIRECTORY

  ;Start Menu Folder Page Configuration
  !define MUI_STARTMENUPAGE_REGISTRY_ROOT "HKCU"
  !define MUI_STARTMENUPAGE_REGISTRY_KEY "Software\${emm_appname}"
  !define MUI_STARTMENUPAGE_REGISTRY_VALUENAME "Start Menu Folder"
  !insertmacro MUI_PAGE_STARTMENU Application $StartMenuFolder

  !insertmacro MUI_PAGE_INSTFILES
  !insertmacro MUI_PAGE_FINISH

  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  UninstPage custom un.UnPageSettings un.UnPageSettingsLeave
  UninstPage custom un.UnPageTemp un.UnPageTempLeave
  !insertmacro MUI_UNPAGE_INSTFILES
  !insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "English"

;--------------------------------
;Install levels

InstType "Portable" ; 1.
;InstType "UserFolder" ; 2.
;InstType "Minimal" ; 3.

;--------------------------------
;Installer Sections

Section "Ember Media Manager" SecEmberMediaManager
  SetShellVarContext current
  SectionIn RO
  SectionIn 1 2 3 #section is in install type Portable/UserFolder/Minimal
  
  ;Cleanup old defaults folder
  RMDir /r "$INSTDIR\Defaults"
  
  ;Cleanup old modules folder
  RMDir /r "$INSTDIR\Modules"
  
  ;Cleanup old files
  Delete "$INSTDIR\*.*"
  
  ;ADD YOUR OWN FILES HERE...
  SetOutPath "$INSTDIR"
  File "${emm_root}\${emm_folder}\${emm_filename}"
  File "${emm_root}\${emm_folder}\${emm_filename}.config"
  File "${emm_root}\${emm_folder}\EmberAPI.dll.config"
  File "${emm_root}\${emm_folder}\NLog.config"
  File "${emm_root}\${emm_folder}\*.xml"
  File "${emm_root}\${emm_folder}\*.dll"
  
  SetOutPath "$INSTDIR\Bin"
  File /r /x *.so "${emm_root}\${emm_folder}\Bin\*.*"
  SetOutPath "$INSTDIR\DB"
  File /r /x *.so "${emm_root}\${emm_folder}\DB\*.*"
  SetOutPath "$INSTDIR\Defaults"
  File /r /x *.so "${emm_root}\${emm_folder}\Defaults\*.*"
  SetOutPath "$INSTDIR\Images"
  File /r /x *.so "${emm_root}\${emm_folder}\Images\*.*"
  SetOutPath "$INSTDIR\Langs"
  File /r /x *.so "${emm_root}\${emm_folder}\Langs\*.*"
  SetOutPath "$INSTDIR\Modules"
  File /r /x *.so "${emm_root}\${emm_folder}\Modules\*.dll"
  File /r /x *.so "${emm_root}\${emm_folder}\Modules\*.xml"
  SetOutPath "$INSTDIR\Modules\Templates"
  File /r /x *.so "${emm_root}\${emm_folder}\Modules\Templates\*.*"
  SetOutPath "$INSTDIR\Modules\x64"
  File /r /x *.so "${emm_root}\${emm_folder}\Modules\x64\*.*"
  SetOutPath "$INSTDIR\Modules\x86"
  File /r /x *.so "${emm_root}\${emm_folder}\Modules\x86\*.*"
  SetOutPath "$INSTDIR\Themes"
  File /r /x *.so "${emm_root}\${emm_folder}\Themes\*.*"
  SetOutPath "$INSTDIR\x64"
  File /r /x *.so "${emm_root}\${emm_folder}\x64\*.*"
  SetOutPath "$INSTDIR\x86"
  File /r /x *.so "${emm_root}\${emm_folder}\x86\*.*"

  ;Turn off overwrite to prevent files in EmberMediaManager\Settings\ from being overwritten
  ;SetOverwrite off

  ;SetOutPath "$INSTDIR\userdata"
  ;File /r /x *.so "${emm_root}\EmberMediaManager\Settings\*.*"

  ;Turn on overwrite for rest of install
  SetOverwrite on

  ;Store installation folder
  WriteRegStr HKCU "Software\${emm_appname}" "" $INSTDIR

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"

  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
  ;Create shortcuts
  SetOutPath "$INSTDIR"

  CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
  CreateShortCut "$SMPROGRAMS\$StartMenuFolder\${emm_appname}.lnk" "$INSTDIR\${emm_filename}" \
    "" "$INSTDIR\${emm_filename}" 0 SW_SHOWNORMAL \
    "" "Start ${emm_appname}."
  CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall ${emm_appname}.lnk" "$INSTDIR\Uninstall.exe" \
    "" "$INSTDIR\Uninstall.exe" 0 SW_SHOWNORMAL \
    "" "Uninstall ${emm_appname}."

  WriteINIStr "$SMPROGRAMS\$StartMenuFolder\Visit ${emm_appname} Online.url" "InternetShortcut" "URL" "http://embermediamanager.org"
  !insertmacro MUI_STARTMENU_WRITE_END

  ;add entry to add/remove programs
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "DisplayName" "${emm_appname}"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "UninstallString" "$INSTDIR\uninstall.exe"
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "NoModify" 1
  WriteRegDWORD HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "NoRepair" 1
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "InstallLocation" "$INSTDIR"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "DisplayIcon" "$INSTDIR\${emm_filename},0"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "Publisher" "Team Ember Media Manager"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "HelpLink" "http://embermediamanager.org"
  WriteRegStr HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}" \
                 "URLInfoAbout" "http://forum.xbmc.org/forumdisplay.php?fid=195"

SectionEnd

Section "Additional Languages" SecAddLangs
  SetShellVarContext current
  ;SectionIn RO
  SectionIn 1 2 3 #section is in install type Portable/UserFolder/Minimal
  ;ADD YOUR OWN FILES HERE...
  SetOutPath "$INSTDIR\Langs"
  File /r /x *.so "${emm_addlangpath}\*.*"
SectionEnd

;SectionGroup "Skins" SecSkins
;Section "Confluence" SecSkinConfluence
  ;SectionIn 1 2 3 #section is in install type Full/Normal/Minimal
  ;SectionIn RO
  ;SetOutPath "$INSTDIR\addons\skin.confluence\"
  ;File /r "${xbmc_root}\Xbmc\addons\skin.confluence\*.*"
;SectionEnd
;skins.nsi is generated by genNsisIncludes.bat
;!include /nonfatal "skins.nsi"
;SectionGroupEnd

;SectionGroup "PVR Addons" SecPvrAddons
;xbmc-pvr-addons.nsi is generated by genNsisIncludes.bat
;!include /nonfatal "xbmc-pvr-addons.nsi"
;SectionGroupEnd

;--------------------------------
;Descriptions

  ;Language strings
  LangString DESC_SecEmberMediaManager ${LANG_ENGLISH} "${emm_appname}"

  ;Assign language strings to sections
  !insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
  !insertmacro MUI_DESCRIPTION_TEXT ${SecEmberMediaManager} $(DESC_SecEmberMediaManager)
  !insertmacro MUI_FUNCTION_DESCRIPTION_END

;--------------------------------
;Uninstaller Section

Var UnPageSettingsDialog
Var UnPageSettingsCheckbox
Var UnPageSettingsCheckbox_State
Var UnPageSettingsEditBox

Function un.UnPageSettings
    !insertmacro MUI_HEADER_TEXT "Uninstall ${emm_appname}" "Remove ${emm_appname}'s settings folder from your computer."
  nsDialogs::Create /NOUNLOAD 1018
  Pop $UnPageSettingsDialog

  ${If} $UnPageSettingsDialog == error
    Abort
  ${EndIf}

  ${NSD_CreateLabel} 0 0 100% 12u "Do you want to delete the settings folder?"
  Pop $0

  ${NSD_CreateText} 0 13u 100% 12u "$INSTDIR\Settings\"
  Pop $UnPageSettingsEditBox
    SendMessage $UnPageSettingsEditBox ${EM_SETREADONLY} 1 0

  ${NSD_CreateLabel} 0 46u 100% 24u "Leave unchecked to keep the settings folder for later use or check to delete the settings folder."
  Pop $0

  ${NSD_CreateCheckbox} 0 71u 100% 8u "Yes, also delete the settings folder."
  Pop $UnPageSettingsCheckbox


  nsDialogs::Show
FunctionEnd

Function un.UnPageSettingsLeave
${NSD_GetState} $UnPageSettingsCheckbox $UnPageSettingsCheckbox_State
FunctionEnd

Var UnPageTempDialog
Var UnPageTempCheckbox
Var UnPageTempCheckbox_State
Var UnPageTempEditBox

Function un.UnPageTemp
    !insertmacro MUI_HEADER_TEXT "Uninstall ${emm_appname}" "Remove ${emm_appname}'s temp folder from your computer."
  nsDialogs::Create /NOUNLOAD 1018
  Pop $UnPageTempDialog

  ${If} $UnPageTempDialog == error
    Abort
  ${EndIf}

  ${NSD_CreateLabel} 0 0 100% 12u "Do you want to delete the temp folder?"
  Pop $0

  ${NSD_CreateText} 0 13u 100% 12u "$INSTDIR\Temp\"
  Pop $UnPageTempEditBox
    SendMessage $UnPageTempEditBox ${EM_SETREADONLY} 1 0

  ${NSD_CreateLabel} 0 46u 100% 24u "Leave unchecked to keep the temp folder for later use or check to delete the temp folder."
  Pop $0

  ${NSD_CreateCheckbox} 0 71u 100% 8u "Yes, also delete the temp folder."
  Pop $UnPageTempCheckbox


  nsDialogs::Show
FunctionEnd

Function un.UnPageTempLeave
${NSD_GetState} $UnPageTempCheckbox $UnPageTempCheckbox_State
FunctionEnd

Section "Uninstall"

  SetShellVarContext current

  ;ADD YOUR OWN FILES HERE...
  Delete "$INSTDIR\${emm_filename}"
  Delete "$INSTDIR\*.dll"
  Delete "$INSTDIR\*.xml"
  Delete "$INSTDIR\*.config"
  RMDir /r "$INSTDIR\Bin"
  RMDir /r "$INSTDIR\DB"
  RMDir /r "$INSTDIR\Defaults"
  RMDir /r "$INSTDIR\Images"
  RMDir /r "$INSTDIR\Langs"
  RMDir /r "$INSTDIR\Log"
  RMDir /r "$INSTDIR\Modules"
  RMDir /r "$INSTDIR\Themes"
  RMDir /r "$INSTDIR\x64"
  RMDir /r "$INSTDIR\x86"

  Delete "$INSTDIR\Uninstall.exe"

;Uninstall Settings folder if option is checked, otherwise skip
  ${If} $UnPageSettingsCheckbox_State == ${BST_CHECKED}
    RMDir /r "$INSTDIR\Settings"
  ${EndIf}
  
;Uninstall Temp folder if option is checked, otherwise skip
  ${If} $UnPageTempCheckbox_State == ${BST_CHECKED}
    RMDir /r "$INSTDIR\Temp"
  ${EndIf}
  
;Uninstall install folder if no settings and no temp folder existing
  ${If} ${FileExists} '$INSTDIR\Settings'
  ${ElseIf} ${FileExists} '$INSTDIR\Temp'
  ${Else}
      RMDir "$INSTDIR"
      DeleteRegKey /ifempty HKCU "Software\${emm_appname}"
  ${EndIf}


  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
  Delete "$SMPROGRAMS\$StartMenuFolder\${emm_appname}.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\Uninstall ${emm_appname}.lnk"
  Delete "$SMPROGRAMS\$StartMenuFolder\Visit ${emm_appname} Online.url"
  RMDir "$SMPROGRAMS\$StartMenuFolder"
  DeleteRegKey HKCU "Software\Microsoft\Windows\CurrentVersion\Uninstall\${emm_appname}"
  DeleteRegKey HKCU "Software\Classes\Directory\shell\EmberMediaManager"
  DeleteRegKey HKCU "Software\Classes\Directory\shell\EmberMediaManager.AddMovieSource"
  DeleteRegKey HKCU "Software\Classes\Directory\shell\EmberMediaManager.AddTVShowSource"
  DeleteRegKey HKCU "Software\Classes\Directory\shell\EmberMediaManager.ScanFolder"
SectionEnd

;--------------------------------
;vs redist installer Section

;Section "Microsoft Visual C++ 2008/2010 Redistributable Package (x86)" SEC_VCREDIST

  ;SectionIn 1 2 #section is in install type Full/Normal and when not installed

  ;DetailPrint "Running VS Redist Setup..."

  ;;vc90 for python
  ;SetOutPath "$TEMP\vc2008"
  ;File "${xbmc_root}\..\dependencies\vcredist\2008\vcredist_x86.exe"
  ;ExecWait '"$TEMP\vc2008\vcredist_x86.exe" /q' $VSRedistSetupError
  ;RMDir /r "$TEMP\vc2008"

  ;;vc100
  ;SetOutPath "$TEMP\vc2010"
  ;File "${xbmc_root}\..\dependencies\vcredist\2010\vcredist_x86.exe"
  ;DetailPrint "Running VS Redist Setup..."
  ;ExecWait '"$TEMP\vc2010\vcredist_x86.exe" /q' $VSRedistSetupError
  ;RMDir /r "$TEMP\vc2010"

  ;DetailPrint "Finished VS Redist Setup"
  ;SetOutPath "$INSTDIR"
;SectionEnd

;--------------------------------
;DirectX webinstaller Section

;!if "${xbmc_target}" == "dx"
;!define DXVERSIONDLL "$SYSDIR\D3DX9_43.dll"

;Section "DirectX Install" SEC_DIRECTX

  ;SectionIn 1 2 #section is in install type Full/Normal and when not installed

  ;DetailPrint "Running DirectX Setup..."

  ;SetOutPath "$TEMP\dxsetup"
  ;File "${xbmc_root}\..\dependencies\dxsetup\dsetup32.dll"
  ;File "${xbmc_root}\..\dependencies\dxsetup\DSETUP.dll"
  ;File "${xbmc_root}\..\dependencies\dxsetup\dxdllreg_x86.cab"
  ;File "${xbmc_root}\..\dependencies\dxsetup\DXSETUP.exe"
  ;File "${xbmc_root}\..\dependencies\dxsetup\dxupdate.cab"
  ;File "${xbmc_root}\..\dependencies\dxsetup\Jun2010_D3DCompiler_43_x86.cab"
  ;File "${xbmc_root}\..\dependencies\dxsetup\Jun2010_d3dx9_43_x86.cab"
  ;ExecWait '"$TEMP\dxsetup\dxsetup.exe" /silent' $DirectXSetupError
  ;RMDir /r "$TEMP\dxsetup"
  ;SetOutPath "$INSTDIR"

  ;DetailPrint "Finished DirectX Setup"

;SectionEnd

;Section "-Check DirectX installation" SEC_DIRECTXCHECK

  ;IfFileExists ${DXVERSIONDLL} +2 0
    ;MessageBox MB_OK|MB_ICONSTOP|MB_TOPMOST|MB_SETFOREGROUND "DirectX9 wasn't installed properly.$\nPlease download the DirectX End-User Runtime from Microsoft and install it again."

;SectionEnd

;Function .onInit
  ;${IfNot} ${AtLeastWinVista}
    ;MessageBox MB_OK|MB_ICONSTOP|MB_TOPMOST|MB_SETFOREGROUND "Windows Vista or above required.$\nThis program can not be run on Windows XP"
    ;Quit
  ;${EndIf}
  ;# set section 'SEC_DIRECTX' as selected and read-only if required dx version not found
  ;IfFileExists ${DXVERSIONDLL} +3 0
  ;IntOp $0 ${SF_SELECTED} | ${SF_RO}
  ;SectionSetFlags ${SEC_DIRECTX} $0
;FunctionEnd
;!endif