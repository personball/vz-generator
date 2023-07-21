#!/bin/bash
rm nupkg/*.nupkg
rm Initializer/samples.zip
7zz a Initializer/samples.zip Initializer/Samples/*
dotnet pack -c Release