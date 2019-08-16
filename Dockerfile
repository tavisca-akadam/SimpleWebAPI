# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY ./SimpleWebAPI/publish ./
ENTRYPOINT ["dotnet", "SimpleWebAPI.dll"]