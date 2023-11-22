#!/bin/bash

test -f .git/hooks/commit-msg && mv .git/hooks/commit-msg .git/hooks/commit-msg.backup.$(date "+%Y%m%d%H%M%S")

cp commit-msg.sh .git/hooks/commit-msg
chmod a+x .git/hooks/commit-msg