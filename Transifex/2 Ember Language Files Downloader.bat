ECHO OFF
CLS
ECHO *******************************************
ECHO *   Language Downloader and Renamer for   *
ECHO *           Ember Media Manager           *
ECHO *******************************************
ECHO *                                         *
ECHO *          !!!!! ATTENTION !!!!!          *
ECHO *                                         *
ECHO *     Existing files will be deleted!     *
ECHO *                                         *
ECHO *******************************************
ECHO *               by DanCooper              *
ECHO *******************************************
PAUSE
RD /s /q .\translations\
CLS
ECHO *******************************************
ECHO *       Copy default Transifex config     *
ECHO *******************************************
copy LangAllConfig.cfg .\.tx\config
CLS
ECHO *******************************************
ECHO *       Get all files from Transifex      *
ECHO *******************************************
tx pull -a
CLS
ECHO *******************************************
ECHO *          Rename files for Ember         *
ECHO *******************************************
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(da_DK):Danish_(da_DK) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(de_DE):German_(de_DE) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(el_GR):Greek_(el_GR) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(es_ES):Spanish_(es_ES) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(fr_FR):French_(fr_FR) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(he_IL):Hebrew_(he_IL) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(it_IT):Italian_(it_IT) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(nl_NL):Dutch_(nl_NL) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(no_NO):Norwegian_(no_NO) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pl_PL):Polish_(pl_PL) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt_BR):Portuguese_(pt_BR) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt_PT):Portuguese_(pt_PT) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(sv_SE):Swedish_(sv_SE) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(vi_VN):Vietnamese_(vi_VN) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(zh_CN.GB2312):Chinese_(zh_CN.GB2312) /EXECUTE /QUIET
CLS
ECHO *******************************************
ECHO *          Copy files for Release         *
ECHO *******************************************
FOR /R ".\translations\Source\1.3.x\Addons" %%f in (*.xml) do xcopy /Y /Q "%%f" ".\translations\Release\1.3.x\Modules\Langs\"
FOR /R ".\translations\Source\1.3.x\Ember Media Manager" %%f in (*.xml) do xcopy /Y /Q "%%f" ".\translations\Release\1.3.x\Langs\"
FOR /R ".\translations\Source\1.4.x\EmberMediaManager" %%f in (*.xml) do xcopy /Y /Q "%%f" ".\translations\Release\1.4.x\Langs\"
CLS
ECHO *******************************************
ECHO *                  DONE!                  *
ECHO *******************************************
PAUSE