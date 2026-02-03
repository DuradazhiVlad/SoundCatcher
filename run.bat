@echo off
echo Checking for .NET SDK...
dotnet --version
if %errorlevel% neq 0 (
    echo Error: .NET SDK is not found. Please install .NET 9 SDK from https://dotnet.microsoft.com/download/dotnet/9.0
    pause
    exit /b
)

echo Restoring dependencies and running SoundCatcher...
dotnet run --project WinFormsApp3.csproj
if %errorlevel% neq 0 (
    echo Error: Failed to run the application.
    pause
)
