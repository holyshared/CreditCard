setup:
	brew install mono
	mkdir ~/bin
	cd bin
	wget https://dist.nuget.org/win-x86-commandline/latest/nuget.exe

build:
	xbuild /p:Configuration=Release CreditCard.sln
	mono ~/bin/nuget.exe pack CreditCard/CreditCard.fsproj -properties Configuration=Release
