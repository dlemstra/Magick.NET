#!/bin/bash

rm -Rf verify
mkdir verify

wget -O verify/Q8.zip https://www.dropbox.com/sh/tycq7qh50zssgr8/AACt2zhBn8GdVlWVxsKzw71Ka?dl=1
unzip verify/Q8.zip -d verify

wget -O verify/Q16.zip https://www.dropbox.com/sh/5xmw2tl3q7a930u/AADN7QlNsvzhFXazc3FrleTYa?dl=1
unzip verify/Q16.zip -d verify

wget -O verify/Q16-HDRI.zip https://www.dropbox.com/sh/tbsisu1byt8csbr/AABv32VeYzU6IBE5EyWL3v9za?dl=1
unzip verify/Q16-HDRI.zip -d verify

docker build -f Linux.Verify.Dockerfile .