rm nupkg/*.nupkg
rm Initializer/samples.zip
7z a .\\Initializer\\samples.zip .\\Initializer\\Samples\\*
dotnet pack -c Release

