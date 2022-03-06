## Features 

# Based on Asad Sahi implementation (https://github.com/asadsahi)

* [ASP.NET Core](http://www.dot.net/)
* [Entity Framework Core](https://docs.efproject.net/en/latest/)
    * Both Sql Server and Sql lite databases are supported (Check installation instrcutions for more details)
* [Angular](https://angular.io/)
* [Angular CLI](https://cli.angular.io/)
* Secure - with CSP and custom security headers
* [SignalR](https://github.com/aspnet/SignalR/) (Chat example)
* [Compodoc](https://compodoc.github.io/compodoc/) for Angular documentation
* Login and Registration functionality using [Asp.Net Identity & JWT](https://docs.asp.net/en/latest/security/authentication/identity.html)
* Token based authentication using [Openiddict](https://github.com/openiddict/openiddict-core)
     * Get public key access using: http://127.0.0.1:5000/.well-known/jwks
* Extensible User/Role identity implementation
* [Angular dynamic forms](https://angular.io/docs/ts/latest/cookbook/dynamic-form.html) for reusability and to keep html code DRY.
* [Swagger](http://swagger.io/) as Api explorer (Visit url **http://127.0.0.1:5000/swagger** OR whatever port visual studio has launched the website.). More [details](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
 
## Pre-requisites

1. [.Net core sdk](https://www.microsoft.com/net/core)
2. Either [VSCode](https://code.visualstudio.com/) with [C#](https://marketplace.visualstudio.com/items?itemName=ms-vscode.csharp) extension OR [Visual studio 2019](https://www.visualstudio.com/)
3. [Nodejs](https://nodejs.org/en/)
4. [MongoDB](https://www.mongodb.com/try/download/community)
5. [PostgresQL](https://www.postgresql.org/download/)

**Make sure you have Node version >= latest LTS and NPM >= latest LTS

## Installation
```

For linux

1. Clone the repo
    git clone https://github.com/SanyaBogos/ListeningUpd.git
2. Change directory
    cd Listening/cmd/dev-linux
3. Restore all packages
    sh build-short.sh
4. Run Server
    sh mongo_start.sh
    sh client_run.sh
    sh dotnet_run.sh

For Windows

1. Clone the repo
    git clone https://github.com/SanyaBogos/ListeningUpd.git
2. Change directory
    cd Listening/cmd/windows
3. Restore all packages
    build-short.bat
4. Run Server
    app_start.bat

    

```

