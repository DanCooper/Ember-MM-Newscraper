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
ECHO *       Delete unused language files      *
ECHO *******************************************
DEL .\translations\(en_CH).xml
DEL .\translations\(en_CH)-Help.xml
CLS
ECHO *******************************************
ECHO *          Rename files for Ember         *
ECHO *******************************************
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(cs):Czech_(cs) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(da_DK):Danish_(da_DK) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(en_AU):English_(en_AU) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(en_GB):English_(en_GB) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(de):German_(de) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(es):Spanish_(es) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(fr):French_(fr) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(he_IL):Hebrew_(he_IL) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(it_IT):Italian_(it_IT) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(nl):Dutch_(nl) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(no):Norwegian_(no) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pl):Polish_(pl) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt):Portuguese_(pt) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt_BR):Portuguese_(pt_BR) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(pt_PT):Portuguese_(pt_PT) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(ro_RO):Romanian_(ro_RO) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(sv):Swedish_(sv) /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(zh):Chinese_(zh) /EXECUTE /QUIET