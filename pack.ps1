rm nupkg/*.nupkg
rm Initializer/samples.zip
cd Initializer
.\zip-samples.ps1
cd ..
dotnet pack -c Release

