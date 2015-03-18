param(
	[string]$Version = $( Read-Host "Input new NxtLib version"),
    [string]$NxtVersion = $( Read-Host "Input supported Nxt version")
)

#Step 1, Update files with new versions
$Nuspec = "NxtLib.nuspec"
$AssemblyInfo = "..\Src\NxtLib\Properties\AssemblyInfo.cs"
$ReadMe = "..\README.md"

(Get-Content $AssemblyInfo) -replace "\[assembly: AssemblyVersion\("".*""\)\]", "[assembly: AssemblyVersion(""$Version"")]" `
    -replace "\[assembly: AssemblyFileVersion\("".*""\)\]", "[assembly: AssemblyFileVersion(""$Version"")]" | `
    Out-File $AssemblyInfo -Encoding utf8

(Get-Content $Nuspec) -replace "<version>.*</version>", "<version>$Version</version>" `
    -replace "NXT version [^ ]+", "NXT version $NxtVersion" | `
    Out-File $Nuspec -Encoding ascii

(Get-Content $ReadMe) -replace "NXT version [^ ]+", "NXT version $NxtVersion" | `
    Out-File $ReadMe -Encoding ascii

#Step 2, build all solutions
$Devenv = "devenv"

&$Devenv "..\Src\NxtLib\NxtLib.csproj" /rebuild Release

#Step 3, Create nuget package
$Nuget = "NuGet.exe"
&$Nuget pack "NxtLib.nuspec"

#Step 4, Commit new version
$Commit = Read-Host 'Commit locally?'
if ($Commit -eq 'y' -or $Commit -eq 'yes')
{
    $Git = "git"
    &$Git commit -a -m "Version $Version"
    &$Git tag v$Version
}

#Step 5, Push nuget package to nuget server
$Push = Read-Host 'Push to nuget?'
if ($Push -ne 'y' -and $Push -ne 'yes')
{
    Exit
}
&$Nuget push NxtLib.$Version.nupkg

#Step 6, Update Example programs with new nuget version
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