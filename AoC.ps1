
function New-Solution {
    param (
        [Parameter(Mandatory=$true, HelpMessage="The name of the new AoC Solution, typically Day04.")]
        [string]
        $ProjectName
    )

    dotnet new aoc-solution -n $ProjectName -o $ProjectName
    dotnet sln AoC2021.sln add "$ProjectName/src/$ProjectName.csproj"
    dotnet sln AoC2021.sln add "$ProjectName/test/$ProjectName.Test.csproj"
}

function Newest-Package($paths) {
    $sorted = $paths | 
        where { $_.Name -match "AoC\.$ProjectName\.\d+\.\d+\.\d+.nupkg" } |
        Select-Object -Property FullName, @{
            Name = 'Version';
            Expression = { [version]$($_.Name | Select-String -Pattern "Aoc.$ProjectName.(\d+.\d+.\d+).nupkg").Matches.Groups[1].ToString() }
        } |
        Sort-Object -Descending -Property 'Version'

    return $sorted[0].FullName
}

function Publish-Lib {
    param (
        [Parameter(Mandatory=$true)]
        [string]
        $ProjectName
    )

    $projectFile = ".\$ProjectName\$ProjectName.csproj"

    dotnet clean $projectFile
    dotnet pack --include-symbols --include-source $ProjectFile
    
    $nugetPath = Newest-Package(ls ".\$ProjectName\bin\debug\*.nupkg")
        
    
    dotnet nuget push -s Local $nugetPath
    $nugetSymbolsPath = $nugetPath -replace ".nupkg$", ".symbols.nupkg"
    dotnet nuget push -s Local $nugetSymbolsPath
    
}

function Set-Current($name) {
    $env:AOC_CUR="$name"
    Write-Host "Set current day to $name"
}

function Get-SolutionName($name) {
    return $name ?? $env:AOC_CUR
}

function Run-Solution($name) {
    $name = Get-SolutionName($name)


    pushd $name/src
    dotnet run
    popd
}

function Build-Solution($name) {
    $name = Get-SolutionName($name)

    dotnet build ./$name/test/$name.Test.csproj
}

function Test-Solution($name) {
    $name = Get-SolutionName($name)

    dotnet test ./$name/test/$name.Test.csproj
}

function AoC() {
    $action = $args[0]
    switch($action) {
        "new" { New-Solution($args[1]) }
        "publish-lib" { Publish-Lib($args[1]) }
        "cur" { Set-Current($args[1]) }
        "build" { Build-Solution($args[1]) }
        "run" { Run-Solution($args[1]) }
        "test" { Test-Solution($args[1]) }
        default {
            Write-Host "Unrecognized command $($action)"
            Write-Host "Commands:"
            Write-Host "    aoc new <Name>"
            Write-Host "    aoc publish-lib <Name>"
            Write-Host ""
            Write-Host "    aoc cur <Name>"
            Write-Host "    aoc build <Name>"
            Write-Host "    aoc run <Name>"
            Write-Host "    aoc test <Name>"
        }
    }
}