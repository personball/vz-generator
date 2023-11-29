#!/usr/bin/env bash
if ! head -1 "$1" | grep -qE "^(feat|fix|ci|chore|docs|test|style|refactor|wip)(\(.+?\))?[\!]?: .{1,}$"; then
    echo "Aborting commit. Your commit message is invalid." >&2
    exit 1
fi
if ! head -1 "$1" | grep -qE "^.{1,80}$"; then
    echo "Aborting commit. Your commit message is too long." >&2
    exit 1
fi