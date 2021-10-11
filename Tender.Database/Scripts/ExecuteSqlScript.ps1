param(
    [Parameter(Mandatory=$true)]
    [string]$sqlScript,
    [Parameter(Mandatory=$true)]
    [string]$sqlServer,
    [Parameter(Mandatory=$true)]
    [string]$database,
    [Parameter(Mandatory=$true)]
    [string]$username,
    [Parameter(Mandatory=$true)]
    [string]$password
)

Invoke-Sqlcmd -InputFile $sqlScript -ServerInstance $sqlServer -Database $database -Username $username -Password $password -EncryptConnection