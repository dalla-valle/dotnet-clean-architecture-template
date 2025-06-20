# Microservice_Name_Token Api

Language(s): C#

Framework(s): .NET Core 9

Team contact: [to-fill]

## Overview

- Api to ...

### Pre-requisites

- Install [.NET core](https://dotnet.microsoft.com/download)

### Run locally

```bash
cd src/Microservice_Name_Token.Api
dotnet watch run
cd src/Microservice_Name_Token.Worker
dotnet watch run
```

### Style guide/ linting

Link to the [editorconfig](.editorconfig).

### Testing

```bash
dotnet test /p:CollectCoverage=true /p:Exclude="[*]Microservice_Name_Token.Infrastructure*"
```

### Logging/ analytics

### Security

### Containerization

Link to the dockerfile:

- [api](C:\_WA\crifrealtime\Microservice_Name_Token\src\Microservice_Name_Token.Api\Dockerfile).

### Pipelines

Link to the [build pipeline](./azure-pipelines.yml).
