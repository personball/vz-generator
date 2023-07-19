#!/bin/bash
rm nupkg/*.nupkg
rm Initializer/samples.zip
zip -q -r Initializer/samples.zip Initializer/Samples
dotnet pack -c Release