##########################################################################################################################
#													*INTRODUCTION*														 #
#																														 #
#	1.Install LibreOffice and find path file soffice.exe. 																 #
# 		- Template Path: C:\Program Files\LibreOffice\program\soffice.exe												 #
#		- Add new variable in Advanced system settings -> Evnironmet Variable -> User variable for User					 #
#		- NAME [SofficePath] VALUE [C:\Program Files\LibreOffice\program\soffice.exe]									 #
#	2.Run Power Shell as admin and run this command 																 #
#		- powershell -ExecutionPolicy Bypass -File .\Exporter.ps1													     #
#																														 #
##########################################################################################################################

$files = Get-ChildItem ".\" -Filter *.ods
$dividerSymbolDecASCICode = 124

$CSVFilesPath = ".\TemporaryCSVs"
$JsonExportsPath = ".\OdsToJsonExports"

if ($null -eq $env:SofficePath)
{
	Write-Error "Aborting export due to missing environmental variables:`nSet SofficePath environment variable to Soffice location (Eg. LibreOffice launcher)"
	exit
}

$SofficeExePath = $Env:SofficePath

    Write-Output "----- Starting CSV files export -----"
	
foreach ($f in $files)
{
    Write-Output "Exporting $f file to csv"
	& $SofficeExePath --headless --convert-to csv:"Text - txt - csv (StarCalc)":$dividerSymbolDecASCICode,34,UTF8,1,0,0,false,true,false,false,false,-1 --outdir $CSVFilesPath $f.FullName | Out-Null
}

$CSVFiles = Get-ChildItem $CSVFilesPath -Filter *.csv

Write-Output "----- Starting Json files export -----"

if (Test-Path -Path $JsonExportsPath)
{
	Write-Output "Json exports folder exists, skipping creation"
}
else
{
	New-Item $JsonExportsPath -ItemType Directory
}

foreach ($csvFile in $CSVFiles)
{
	$csvFileFullName = $csvFile.FullName
	$fullJsonFileName = "$JsonExportsPath\$csvFile"
	Write-Output "Exporting $csvFileFullName file to json"

	import-csv $csvFileFullName -Delimiter "|" | ConvertTo-Json -Compress | Set-Content -Path "$fullJsonFileName.json" | Out-Null
}

$CSVFiles = Get-ChildItem $CSVFilesPath -Filter *.csv
Write-Output "----- Starting CSV files removal -----"

Remove-Item -Recurse -Force "$CSVFilesPath" | Out-Null