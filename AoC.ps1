
function New-Solution {
    param (
        [Parameter(Mandatory=$true, HelpMessage="The name of the new AoC Solution, typically D04.")]
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

function AoC() {
    $action = $args[0]
    switch($action) {
        "new" { New-Solution($args[1]) }
        "publish-lib" { Publish-Lib($args[1]) }
        default {
            Write-Host "Unrecognized command $($action)"
            Write-Host "Commands:"
            Write-Host "    aoc new <Name>"
            Write-Host "    aoc publish-lib <Name>"
        }
    }
}