# Credit Card

[![Build status](https://ci.appveyor.com/api/projects/status/0m9xb4to8a1rkk4b?svg=true)](https://ci.appveyor.com/project/holyshared/creditcard)

## Basic usage

Detects the applicable brand from the card number.
The card number may be masked, you should use the masked number as much as possible to use the card number safely.

```fsharp
open CreditCard.Brand

match Detect "4111************" with
	| Some brand -> printfn "%s" brand.Name
	| None -> printfn "The card brand was not found"
```

```fsharp
open CreditCard.Brand

match DetectFrom [VISA; MasterCard] "4111************" with
	| Some brand -> printfn "%s" brand.Name
	| None -> printfn "The card brand was not found"
```

Be especially careful when handling card numbers on the server side, there is a risk of letting out the card number out.

## Support card brand

Currently this package can identify the following card brands.

* VISA
* MasterCard
* AmericanExpress
* JCB
* DinersClub

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
