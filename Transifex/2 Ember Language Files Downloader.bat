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
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(da):Danish /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(de):German /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(el):Greek /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(es):Spanish /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(fr):French /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(he):Hebrew /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(it):Italian /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(nl):Dutch /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(no):Norwegian /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pl):Polish /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt_BR):Portuguese(Brazil) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt_PT):Portuguese(Portugal) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(sv):Swedish /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(vi):Vietnamese /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(zh):Chinese /EXECUTE /QUIET
CLS
ECHO *******************************************
ECHO *                  DONE!                  *
ECHO *******************************************
PAUSE