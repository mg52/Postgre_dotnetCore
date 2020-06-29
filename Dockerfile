FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /PostgreTest
COPY /PostgreTest/Publish .
ENTRYPOINT ["dotnet", "PostgreTest.dll"]