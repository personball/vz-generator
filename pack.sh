#!/bin/bash

# rm -rf .vz
rm -rf .vzx
rm nupkg/*.nupkg

rm Initializer/samples.zip
cd Initializer/Samples
zip -q -r ../samples.zip . 
cd ../../

resgen Localization/Resources/VzLocales.zh-Hans.txt 
resgen Localization/Resources/VzLocales.txt

dotnet pack -c Release