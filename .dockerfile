FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /source

COPY *.sln .

COPY RazorPagesApp/*.csproj ./RazorPagesApp/
COPY DAL/*.csproj ./DAL/
COPY Core/*.csproj ./Core/
COPY Application/*.csproj ./Application/

RUN dotnet restore

COPY RazorPagesApp/. ./RazorPagesApp/
COPY DAL/. ./DAL/
COPY Core/. ./Core/
COPY Application/. ./Application/

WORKDIR /source

RUN dotnet build -c Release --no-restore

WORKDIR /source/RazorPagesApp

RUN dotnet publish -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app ./

ENTRYPOINT ["dotnet", "QuiApp.WebMVC.dll"]