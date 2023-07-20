rm nupkg/*.nupkg
rm Initializer/samples.zip
Compress-Archive -Path Initializer/Samples -DestinationPath Initializer/samples.zip
dotnet pack -c Release

