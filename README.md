<a href="http://flattr.com/thing/1321788/" target="_blank"><img src="http://api.flattr.com/button/flattr-badge-large.png" alt="Flattr this" title="Flattr this" border="0" /></a>

# Ember Media Manager

We decided that was time to give Ember a new home. We've taken it upon ourselves not only to pick up the code where it was left off but to attempt to continue it's development.

If you found our work useful feel free to [donate](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=VWVJCUV3KAUX2&lc=CH&item_name=Ember%2dTeam%3a%20DanCooper%2c%20m%2esavazzi%20%26%20Cocotus&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted) us a beer!

[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_SM.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=VWVJCUV3KAUX2&lc=CH&item_name=Ember%2dTeam%3a%20DanCooper%2c%20m%2esavazzi%20%26%20Cocotus&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHosted)

## Goals
To continue development of EmberMM, because its a great product, that in my opinion is the most stable and useful media manager available, I've tried all others, but yet still come back to Ember.

## Links
- Main discussion : http://forum.xbmc.org/showthread.php?tid=116941
- GitHub : https://github.com/DanCooper/Ember-MM-New (DanCooper is mainaining the most aligned version)

## Helping the development
Any help is more than welcome. We do suggest everyone to participate in the forum to be aligned and updated.

As the codebase is managed by several people we tried to make it easier to maintain and review. We ask everyone to try to adhere to some simple guidelines as much as you can:
- keep it simple, if complexity is needed add a comment to explain why
- avoid duplication of code, if mandatory or needed please comment
- read all the code before changing it, avoid duplication of almost identical functionalities/classes/data. In case of doubt, please ask

_(We know everyone knows and agrees on them but the more we work on the code the more we discover how those simple principles has not been applied even from us... )_

We made a major effort in reviewing the core of Ember Media Manager, the scraping process and part, to bring it to the next level. Here are major points to consider:
- IMDB id is the unique identifier for movies.
- It is INTENTIONAL to separate the scrapers in three groups (Data, Poster, Trailer). We decided that the small overhead of code in the modules manager and some duplication of code was a far minor issue than the complexity (or mess) that had evolved in the multipurpose scrapers, making it complex to fix and almost impossible to add new ones quickly enough.
- Data scrapers will be executed one after the other and will fill ONLY selected & empty fields if not locked from global properites
- Each Data scraper will have the search dialog (is a known and accepted code duplication) because there are TOO many differences between IMDB, TMDB and other so having only one dialog in main would lead to a mess.
- Image scrapers will work in parallel and will return a list of images. The image selection dialog will merge all lists and show them. The dialog will be moved at main program level as is useless to have it replicated in the scrapers
- Order in Image scrapers will only be used for automated scraping where only the first one will be invoked (to be quicker)
- All the file save-handling logic with the names etc... will be put at main program level and will happen only once.
- All image Handling (load-save-fromWEb, etc) MUST be in only in the Images class and must use the memorystream as source (already almost there in 1.3.0.12)
- Trailers should behave as images


## Contact
Please use the forum as main contact point.
