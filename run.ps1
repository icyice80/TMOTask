# Enable strict mode and stop on error
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

Write-Host "Starting build and run process..."

# Step 1: Navigate to the .NET app and build
Write-Host "`nBuilding .NET app..."
cd "./TmoTask"
dotnet build

# Step 2: Run the .NET app in the background
Write-Host "Running .NET app..."
Start-Process "dotnet" "run --launch-profile http" -WorkingDirectory (Get-Location)

# Wait a bit to ensure the app has started
Start-Sleep -Seconds 20

# Step 3: Open the browser manually
Start-Process "http://localhost:5139/swagger"

# Step 4: Navigate to the React app
Write-Host "`nStarting React app..."
cd "../tmo-react"

# Step 5: Install dependencies (optional, first-time only)
if (!(Test-Path "./node_modules")) {
  Write-Host "Installing React dependencies..."
  npm install
}

# Step 5: Run the React dev server
Start-Process "npm" "start" -WorkingDirectory (Get-Location)

cd ".."

Write-Host "Both apps are running!"