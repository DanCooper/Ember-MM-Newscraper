:: follow the instruction here to create a link to the upstream repository
:: https://help.github.com/articles/syncing-a-fork
:: then this batch will sync with upstream
git fetch upstream
git checkout 1.3.0.x
git merge upstream/1.3.0.x
