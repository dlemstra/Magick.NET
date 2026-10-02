#!/bin/bash

architecture=$1
quantum=$2

config=Test$quantum

export FONTCONFIG_PATH=/etc/fonts
export FONTCONFIG_FILE=/etc/fonts/fonts.conf

run_test() {
    local binary=$1

    "$binary"
    local exit_code=$?

    if [ $exit_code -eq 139 ]; then
        echo "=== Segmentation fault detected ==="
        dmesg | tail -30

        core_file=$(find . -name "core*" -type f 2>/dev/null | head -1)
        if [ -n "$core_file" ] && command -v gdb >/dev/null 2>&1; then
            echo "=== GDB Stack Trace ==="
            gdb -batch -ex "bt full" -ex "quit" "$binary" "$core_file"
        fi
    fi

    if [ $exit_code -ne 0 ]; then
        exit $exit_code
    fi
}

run_test ./tests/Magick.NET.Core.Tests/bin/$config/$architecture/net10.0/Magick.NET.Core.Tests
run_test ./tests/Magick.NET.Tests/bin/$config/$architecture/net10.0/Magick.NET.Tests
