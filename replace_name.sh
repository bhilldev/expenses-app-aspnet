#!/bin/bash
# replace_name.sh
# Replaces all occurrences of "ExpensesApp" with "ExpensesApp"
# across the entire project (except .git folder)

SEARCH="ExpensesApp"
REPLACE="ExpensesApp"

# Run replacement on everything except .git
find . -type f \
  ! -path "./.git/*" \
  -exec sed -i "s/$SEARCH/$REPLACE/g" {} +

echo "✅ Replaced all occurrences of '$SEARCH' with '$REPLACE' across project"
