﻿# Paths and variables
$files = Get-ChildItem ".\" -Filter *.ods
$dividerSymbolDecASCICode = 124

$CSVFilesPath = ".\TemporaryCSVs"
$JsonExportsPath = ".\OdsToJsonExports"

# Check if SofficePath environment variable is set
if ($null -eq $env:SofficePath) {
    Write-Error "Aborting export due to missing environmental variables:`nSet SofficePath environment variable to Soffice location (e.g., LibreOffice launcher)"
    exit
}

$SofficeExePath = $env:SofficePath

# Suppress output by redirecting to null
$null = Write-Output "----- Starting CSV files export -----"

foreach ($f in $files) {
    $baseFileName = [System.IO.Path]::GetFileNameWithoutExtension($f.Name)
    $fileCSVPath = "$CSVFilesPath\$baseFileName"
    
    if (-not (Test-Path -Path $fileCSVPath)) {
        New-Item -Path $fileCSVPath -ItemType Directory | Out-Null
    }
    
    # Suppress output by redirecting to null
    $null = Write-Output "Exporting $f to CSV"
    & $SofficeExePath --headless --convert-to csv:"Text - txt - csv (StarCalc)":$dividerSymbolDecASCICode,34,UTF8,1,0,0,false,true,false,false,false,-1 --outdir $fileCSVPath $f.FullName | Out-Null
    
    # Collect CSV files for this ODS file
    $CSVFiles = Get-ChildItem $fileCSVPath -Filter *.csv

    # Create a hashtable to hold the JSON structure
    $jsonData = @{}

    foreach ($csvFile in $CSVFiles) {
        $csvFileFullName = $csvFile.FullName
        $sheetName = [System.IO.Path]::GetFileNameWithoutExtension($csvFile.Name)

        # Remove the base file name and the hyphen from the sheet name
        $cleanSheetName = $sheetName -replace "^$baseFileName-", ""
        $null = Write-Output "Exporting $csvFileFullName to JSON with sheet name $cleanSheetName"

        $csvContent = Import-Csv $csvFileFullName -Delimiter "|"

        # Process each row to convert empty strings to $null
        $csvContent = $csvContent | ForEach-Object {
            $row = $_
            $row.PSObject.Properties | ForEach-Object {
                if ($_.Value -eq "") {
                    $_.Value = $null
                }
            }
            $row
        }

        # Grouping similar entities in arrays based on naming conventions
        if ($cleanSheetName -eq "Recipes") {
            $jsonData["Recipes"] = @($csvContent)
        } elseif ($cleanSheetName -eq "Crafting_tables") {
            $jsonData["Crafting_tables"] = @($csvContent)
        } elseif ($cleanSheetName -eq "itemChest") {
            $jsonData["itemChest"] = @($csvContent)
        } elseif ($cleanSheetName -eq "Items") {
            $jsonData["Items"] = @($csvContent)
        } elseif ($cleanSheetName -eq "itemChanger") {
            $jsonData["itemChanger"] = @($csvContent)
        } else {
            # If the sheet name is not matched, just add it normally
            $jsonData[$cleanSheetName] = @($csvContent)
        }
    }

    # Convert the hashtable to JSON and save it
    $fullJsonFileName = "$JsonExportsPath\$baseFileName.json"
    $jsonData | ConvertTo-Json -Compress | Set-Content -Path $fullJsonFileName -Force | Out-Null
}

# Suppress output by redirecting to null
$null = Write-Output "----- Starting CSV files removal -----"
Remove-Item -Recurse -Force "$CSVFilesPath" | Out-Null
