
# Set Working Directory
Split-Path $MyInvocation.MyCommand.Path | Push-Location
[Environment]::CurrentDirectory = $PWD

./Publish.ps1 -ProjectPath "Source/sonicheroes.utils.unlimitedobjectdrawdistance.csproj" `
              -PackageName "sonicheroes.utils.unlimitedobjectdrawdistance" `

Pop-Location