param(
	[string]$Version = $( Read-Host "Input new NxtLib version"),
    [string]$NxtVersion = $( Read-Host "Input supported Nxt version")
)

#Step 1, Update files with new versions
$ProjectJson = "..\Src\NxtLib\project.json"
$ReadMe = "..\README.md"
$Constants = "..\Src\NxtLib\Local\Constants.cs"

(Get-Content $Constants) -replace "NxtVersionSupport = "".*"";", "NxtVersionSupport = ""$NxtVersion"";" | `
    Out-File $Constants -Encoding utf8

(Get-Content $ProjectJson) -replace """version"": "".*""", """version"": ""$Version""" `
    -replace "It currently supports NXT version .*""", "It currently supports NXT version $NxtVersion""" | `
    Out-File $ProjectJson -Encoding utf8

(Get-Content $ReadMe) -replace "NXT version [^ ]+", "NXT version $NxtVersion" | `
    Out-File $ReadMe -Encoding ascii

#Step 2, restore packages
&dnu restore "..\Src\NxtLib"

#Step 3, build and package
&dnu pack "..\Src\NxtLib" --configuration Release --out "..\Src\artifacts\bin\NxtLib"

#Step 3, Commit new version
$Commit = Read-Host 'Commit locally?'
if ($Commit -eq 'y' -or $Commit -eq 'yes')
{
    $Git = "git"
    &$Git commit -a -m "Version $Version"
    &$Git tag v$Version
}

#Step 4, Push nuget package to nuget server
$Push = Read-Host 'Push to nuget?'
if ($Push -ne 'y' -and $Push -ne 'yes')
{
    Exit
}
$Nuget = "nuget"
&$Nuget push "..\Src\artifacts\bin\NxtLib\Release\NxtLib.$Version.nupkg"

#Script does not work beyond here, fix! Until then, exit.
Exit

#Step 5, Update Example programs with new nuget version
$Upgrade = Read-Host 'Upgrade Example projects?'
if ($Upgrade -ne 'y' -and $Upgrade -ne 'yes')
{
    Exit
}

Exit

#TODO - fix this shit
$NxtConsoleDemo = "..\Examples\NxtConsoleDemo\"
$ReservableCurrenciesDemo = "..\Examples\NxtConsoleDemo\NxtConsoleDemo\"

$CsProjFiles = Get-ChildItem -Path "$NxtLibPath\Examples\" -Filter *.csproj -Recurse