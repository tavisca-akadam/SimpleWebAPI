# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY publish .
ARG APPLICATION
ENV APP_NAME = ${APPLICATION}
ENTRYPOINT dotnet "${APP_NAME}.dll"