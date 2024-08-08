# Paths and variables
$files = Get-ChildItem ".\" -Filter *.ods
$dividerSymbolDecASCICode = 124

$CSVFilesPath = ".\TemporaryCSVs"
$JsonExportsPath = ".\OdsToJsonExports"

# Check if SofficePath environment variable is set
if ($null -eq $env:SofficePath)
{
    Write-Error "Aborting export due to missing environmental variables:`nSet SofficePath environment variable to Soffice location (Eg. LibreOffice launcher)"
    exit
}

$SofficeExePath = $Env:SofficePath

Write-Output "----- Starting CSV files export -----"

foreach ($f in $files)
{
    $baseFileName = [System.IO.Path]::GetFileNameWithoutExtension($f.Name)
    $fileCSVPath = "$CSVFilesPath\$baseFileName"
    
    if (-Not (Test-Path -Path $fileCSVPath))
    {
        New-Item -Path $fileCSVPath -ItemType Directory
    }
    
    Write-Output "Exporting $f file to csv"
    & $SofficeExePath --headless --convert-to csv:"Text - txt - csv (StarCalc)":$dividerSymbolDecASCICode,34,UTF8,1,0,0,false,true,false,false,false,-1 --outdir $fileCSVPath $f.FullName | Out-Null
    
    # Collect CSV files for this ODS file
    $CSVFiles = Get-ChildItem $fileCSVPath -Filter *.csv

    # Create a dictionary to hold the JSON structure
    $jsonData = @{}

    foreach ($csvFile in $CSVFiles)
    {
        $csvFileFullName = $csvFile.FullName
        $sheetName = [System.IO.Path]::GetFileNameWithoutExtension($csvFile.Name)

        # Remove the base file name and the hyphen from the sheet name
        $cleanSheetName = $sheetName -replace "^$baseFileName-", ""
        Write-Output "Exporting $csvFileFullName file to json with sheet name $cleanSheetName"

        $csvContent = Import-Csv $csvFileFullName -Delimiter "|"

        # Process each row to convert empty strings to null
        $csvContent = $csvContent | ForEach-Object {
            $row = $_
            $row.PSObject.Properties | ForEach-Object {
                if ($_.Value -eq "") {
                    $_.Value = $null
                }
            }
            $row
        }

        # If the sheet is supposed to be an array (like Tools), convert it to an array
        if ($cleanSheetName -eq "Tools" -or $cleanSheetName -eq "toolChest")
        {
            $jsonData[$cleanSheetName] = @($csvContent)
        }
        else
        {
            $jsonData[$cleanSheetName] = $csvContent
        }
    }

    # Convert the dictionary to JSON and save it
    $fullJsonFileName = "$JsonExportsPath\$baseFileName.json"
    $jsonData | ConvertTo-Json -Compress | Set-Content -Path $fullJsonFileName | Out-Null
}

Write-Output "----- Starting CSV files removal -----"
Remove-Item -Recurse -Force "$CSVFilesPath" | Out-Null
