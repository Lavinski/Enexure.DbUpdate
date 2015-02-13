Import-Module "$PSScriptRoot\modules\msbuild\Invoke-MsBuild.psm1"

$nuget = "$PSScriptRoot\nuget\nuget.exe"

properties {
	$solutionDir = Resolve-Path "$PSScriptRoot\.."
	$projectName = "Enexure.DbUpdate"
}

task default -depends Version

task HowdItGo {
	ls $solutionDir *.dll -r | % versioninfo
}

task Version {

	$versionSourceFile = "$solutionDir\src\$projectName\Version.json"
	$versionSourceFileContents = [string](Get-Content $versionSourceFile)
	$versionDetail = ConvertFrom-Json $versionSourceFileContents
	$version = "$($versionDetail.Major).$($versionDetail.Minor).$($versionDetail.Patch).$build"

	Write-Host "Version: $version"
	
	$projectDir = "$solutionDir\src\$projectName\Properties"
	$versionFile = "$projectDir\AssemblyVersion.cs"

	# Version information for an assembly consists of the following four values:
	# 
	#      Major Version
	#      Minor Version 
	#      Build Number
	#      Revision
	# 
	# You can specify all the values or you can default the Build and Revision Numbers 
	# by using the '*' as shown below:
	# [assembly: AssemblyVersion("1.0.*")]
	
	$versionFileContents = 
	"using System.Reflection;" + "`n" +
	"[assembly: AssemblyVersion(`"$version`")]" + "`n" +
	"[assembly: AssemblyFileVersion(`"$version`")]"

	Set-Content $versionFile $versionFileContents
	
	Write-Host "File: $versionFile saved"	
}

task ? -Description "Helper to display task info" {
	Write-Documentation
}
