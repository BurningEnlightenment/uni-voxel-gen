@echo off & setlocal

if not exist build mkdir build
bin\protoc --csharp_out=.\build\ "tnt-world.proto"

:: copying logic... ugly as heck, but works...
set genDir="..\src\UniDortmund.FaProSS17P3G1.MapGenerator\Model"
set unityDir="..\unity\Assets\Scripts\Model"

if not exist %genDir% mkdir %genDir%
xcopy build\* %genDir% /Y /R
if not exist %unityDir% mkdir %unityDir%
xcopy build\* %unityDir% /Y /R

Pause
