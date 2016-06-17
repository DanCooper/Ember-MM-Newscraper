ECHO OFF
rem build settings for x86
rem EMM_FILENAME must match the Emm-exe
SET EMM_FILENAME=Ember Media Manager.exe
SET EMM_APPNAME=Ember Media Manager BETA
SET EMM_ROOT=%CD%\..
SET EMM_FOLDER=EmberMM - Release - x86
SET EMM_SYSTEM=x86
SET EMM_REVISION=1.4.8.0-alpha16
SET EMM_BRANCH=Master
SET EMM_SETUPFILE=%EMM_APPNAME% %EMM_REVISION% %EMM_SYSTEM%.exe
SET EMM_OUTFILE=Builds\%EMM_SETUPFILE%
SET EMM_ADDLANGPATH=.\translations
CLS
ECHO *******************************************
ECHO *         Build Installer Script          *
ECHO *         Ember Media Manager x86         *
ECHO *******************************************
ECHO *                                         *
ECHO *          !!!!! ATTENTION !!!!!          *
ECHO *                                         *
ECHO *     Existing files will be deleted!     *
ECHO *                                         *
ECHO *******************************************
ECHO *               by DanCooper              *
ECHO *******************************************
rem check if an Transifex account is existing
IF NOT EXIST %HOMEPATH%\.transifexrc CALL txAccount.bat
rem downloading additional language files from transifex
CALL txDownload.bat
CLS
ECHO *******************************************
ECHO *           Get NSIS EXE Path...          *
ECHO *******************************************
  rem get path to makensis.exe from registry, first try tab delim
  FOR /F "tokens=2* delims= " %%A IN ('REG QUERY "HKLM\Software\NSIS" /ve') DO SET NSISExePath=%%B

  IF NOT EXIST "%NSISExePath%" (
    rem try with space delim instead of tab
    FOR /F "tokens=2* delims= " %%A IN ('REG QUERY "HKLM\Software\NSIS" /ve') DO SET NSISExePath=%%B
  )
      
  IF NOT EXIST "%NSISExePath%" (
    rem fails on localized windows (Default) becomes (Par D?faut)
    FOR /F "tokens=3* delims= " %%A IN ('REG QUERY "HKLM\Software\NSIS" /ve') DO SET NSISExePath=%%B
  )

  IF NOT EXIST "%NSISExePath%" (
    FOR /F "tokens=3* delims= " %%A IN ('REG QUERY "HKLM\Software\NSIS" /ve') DO SET NSISExePath=%%B
  )
  
  rem proper x64 registry checks
  IF NOT EXIST "%NSISExePath%" (
    ECHO using x64 registry entries
    FOR /F "tokens=2* delims= " %%A IN ('REG QUERY "HKLM\Software\Wow6432Node\NSIS" /ve') DO SET NSISExePath=%%B
  )
  IF NOT EXIST "%NSISExePath%" (
    rem try with space delim instead of tab
    FOR /F "tokens=2* delims= " %%A IN ('REG QUERY "HKLM\Software\Wow6432Node\NSIS" /ve') DO SET NSISExePath=%%B
  )
  IF NOT EXIST "%NSISExePath%" (
    rem on win 7 x64, the previous fails
    FOR /F "tokens=3* delims= " %%A IN ('REG QUERY "HKLM\Software\Wow6432Node\NSIS" /ve') DO SET NSISExePath=%%B
  )
  IF NOT EXIST "%NSISExePath%" (
    rem try with space delim instead of tab
    FOR /F "tokens=3* delims= " %%A IN ('REG QUERY "HKLM\Software\Wow6432Node\NSIS" /ve') DO SET NSISExePath=%%B
  )

CLS
ECHO *******************************************
ECHO *         Creating setup file...          *
ECHO *             PLEASE WAIT                 *
ECHO *******************************************
IF NOT EXIST Builds MD Builds
SET NSISExe=%NSISExePath%\makensis.exe
"%NSISExe%" /V1 /X"SetCompressor /FINAL lzma" /Demm_addlangpath="%EMM_ADDLANGPATH%" /Demm_filename="%EMM_FILENAME%" /Demm_appname="%EMM_APPNAME%" /Demm_root="%EMM_ROOT%" /Demm_folder="%EMM_FOLDER%" /Demm_outfile="%EMM_OUTFILE%" "Beta_1.4_InstallerScript.nsi"
ECHO ************************************************
ECHO DONE!
ECHO.
ECHO Setup is located at
ECHO %CD%\Builds\%EMM_SETUPFILE%
ECHO ************************************************
PAUSE