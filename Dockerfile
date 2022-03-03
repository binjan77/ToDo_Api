#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#RUN cp /etc/ssl/openssl.cnf /etc/ssl/openssl.cnf.ORI && \
    #sed -i "s/\(MinProtocol *= *\).*/\1TLSv1.0 /" "/etc/ssl/openssl.cnf" && \
    #sed -i "s/\(CipherString *= *\).*/\1DEFAULT@SECLEVEL=1 /" "/etc/ssl/openssl.cnf" && \
    #(diff -u /etc/ssl/openssl.cnf.ORI /etc/ssl/openssl.cnf || exit 0)
RUN sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf
WORKDIR /app
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5101

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ToDo-API.csproj", "ToDo-API/"]
RUN dotnet restore "ToDo-API/ToDo-API.csproj"
COPY . .
WORKDIR "/src/ToDo-API"
RUN dotnet build "ToDo-API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToDo-API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDo-API.dll"]
