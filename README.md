# Credit Card

[![Build status](https://ci.appveyor.com/api/projects/status/0m9xb4to8a1rkk4b?svg=true)](https://ci.appveyor.com/project/holyshared/creditcard)

## Build

### MacOS

#### NuGet package

1. Installing NuGet & Mono Framework

		brew install mono

		mkdir ~/bin
		cd bin
		wget https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

2. Build projects

		xbuild /p:Configuration=Release CreditCard.sln

2. Build NuGet package

		mono ~/bin/nuget.exe pack CreditCard/CreditCard.fsproj -properties Configuration=Release

or 

	make setup
	make build
