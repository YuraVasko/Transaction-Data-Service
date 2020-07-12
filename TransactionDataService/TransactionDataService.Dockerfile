FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app/

COPY ./Transaction.Data.Service.Host/. ./Transaction.Data.Service.Host/.
COPY ./Transaction.Data.Service.API/. ./Transaction.Data.Service.API/.
COPY ./Transaction.Data.Service.BLL/. ./Transaction.Data.Service.BLL/.
COPY ./Transaction.Data.Service.DAL/. ./Transaction.Data.Service.DAL/.
COPY ./Transaction.Data.Service.DTO/. ./Transaction.Data.Service.DTO/.

RUN dotnet restore /app/Transaction.Data.Service.Host/Transaction.Data.Service.Host.csproj

RUN dotnet publish Transaction.Data.Service.Host/Transaction.Data.Service.Host.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS transaction-data-service
WORKDIR /app
COPY --from=build /app/publish ./
EXPOSE 8080

ENTRYPOINT ["dotnet", "Transaction.Data.Service.Host.dll"]