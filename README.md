# Sponge Runtime

<p align="left">
    <a target="_blank" href="https://github.com/root-square/sponge/actions"><img alt="GitHub Workflow Status" src="https://img.shields.io/github/actions/workflow/status/root-square/sponge/publish.yml?branch=main"></a>
    <a target="_blank" href="https://github.com/root-square/sponge/releases/latest"><img alt="GitHub release (latest by date)" src="https://img.shields.io/github/v/release/root-square/sponge"></a>
    <a target="_blank" href="https://github.com/root-square/sponge/blob/main/docs/LICENSE.md"><img alt="GitHub" src="https://img.shields.io/github/license/root-square/sponge"></a>
</p>

## Docs
[See the documentation](./docs/GUIDE.md) for instructions on how to use it.

## Build
### Requirements
 * __OS__ : Windows 10 or higher version
 * __Tools__
   * [PowerShell 7](https://github.com/PowerShell/PowerShell)
   * [Visual Studio 2022](https://visualstudio.microsoft.com/)(include 'Desktop development with C++' and '.NET desktop development' workload)
   * [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)

### Guide
1. Open a PowerShell terminal from the project directory.
2. Run the commands below.
```pwsh
Set-ExecutionPolicy Unrestricted
./tools/publish.ps1
```

## License
The contents are freely available under the [MIT License](http://opensource.org/licenses/MIT).

The licenses of third-party libraries can be found [here](./docs/OPENSOURCES.md).
