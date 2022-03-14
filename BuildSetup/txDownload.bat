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
RD /s /q .\translations
RD /s /q .\Transifex
ECHO *******************************************
ECHO *       Copy default Transifex config     *
ECHO *******************************************
copy LangAllConfig.cfg .\.tx\config
ECHO *******************************************
ECHO *       Get all files from Transifex      *
ECHO *******************************************
tx pull --all --minimum-perc=40
ECHO *******************************************
ECHO *          Rename files for Ember         *
ECHO *******************************************
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:(: /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:): /EXECUTE /QUIET
BRC32 /NOFOLDERS /RECURSIVE /PATTERN:"*.xml" /REPLACECS:_:- /EXECUTE /QUIET