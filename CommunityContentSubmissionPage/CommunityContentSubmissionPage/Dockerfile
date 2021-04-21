FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app 

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Debug -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0


COPY ./wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

COPY ./wait-for /wait-for
RUN chmod +x /wait-for

WORKDIR /app
COPY --from=build-env /app/out .

RUN apt-get update \
    && apt-get install -y --no-install-recommends netcat \
    && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "CommunityContentSubmissionPage.dll"]