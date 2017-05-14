# Credit Card

[![Build status](https://ci.appveyor.com/api/projects/status/0m9xb4to8a1rkk4b?svg=true)](https://ci.appveyor.com/project/holyshared/creditcard)

## Build

### MacOS

#### NuGet package

1. Installing NuGet & Mono Framework

		brew install mono
		brew install nuget

2. Build NuGet package

		mono [nuget.exe path] pack CreditCard/CreditCard.fsproj -properties Configuration=Release
