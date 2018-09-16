#!/bin/bash

#if git diff --name-only HEAD~1 HEAD | grep Checkout.sh; then
    echo "##vso[task.setvariable variable=BuildLinux]true"
#fi