FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 64356
EXPOSE 44392

FROM microsoft/dotnet:2.1-sdk AS build
#setup node
ENV NODE_VERSION 8.11.1
ENV NODE_DOWNLOAD_SHA 0e20787e2eda4cc31336d8327556ebc7417e8ee0a6ba0de96a09b0ec2b841f60
RUN apt-get update && apt-get install curl -y
RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

WORKDIR /src
COPY src/Listening.Web/Listening.Web.csproj src/Listening.Web/
COPY src/Listening.Core/Listening.Core.csproj src/Listening.Core/
COPY src/Listening.Infrastructure/Listening.Infrastructure.csproj src/Listening.Infrastructure/
RUN dotnet restore src/Listening.Web/Listening.Web.csproj

COPY src/Listening.Web/ClientApp/package.json src/Listening.Web/ClientApp/
RUN npm i -g yarn
RUN cd src/Listening.Web/ClientApp \
    && yarn
COPY . .
WORKDIR /src/src/Listening.Web
RUN dotnet build Listening.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Listening.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app

#setup node, this is only needed if you use Node both at runtime and build time. Some people may only need the build part.
ENV NODE_VERSION 8.11.1
ENV NODE_DOWNLOAD_SHA 0e20787e2eda4cc31336d8327556ebc7417e8ee0a6ba0de96a09b0ec2b841f60
RUN curl -SL "https://nodejs.org/dist/v${NODE_VERSION}/node-v${NODE_VERSION}-linux-x64.tar.gz" --output nodejs.tar.gz \
    && echo "$NODE_DOWNLOAD_SHA nodejs.tar.gz" | sha256sum -c - \
    && tar -xzf "nodejs.tar.gz" -C /usr/local --strip-components=1 \
    && rm nodejs.tar.gz \
    && ln -s /usr/local/bin/node /usr/local/bin/nodejs

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Listening.Web.dll"]
