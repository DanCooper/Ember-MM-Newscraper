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
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(da):Danish_(da) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(de):German_(de) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(el):Greek_(el) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(es):Spanish_(es) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(fr):French_(fr) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(he):Hebrew_(he) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(it):Italian_(it) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(nl):Dutch_(nl) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(no):Norwegian_(no) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pl):Polish_(pl) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt_BR):Portuguese_(pt_BR) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt_PT):Portuguese_(pt_PT) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(sv):Swedish_(sv) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(vi):Vietnamese_(vi) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(zh):Chinese_(zh) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(ro_RO):Romanian_(ro_RO) /EXECUTE /QUIET