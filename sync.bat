:: follow the instruction here to create a link to the upstream repository
:: https://help.github.com/articles/syncing-a-fork
:: then this batch will sync with upstream
git fetch upstream
git checkout master
git merge upstream/master
