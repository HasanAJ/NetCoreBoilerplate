## About The Project

The goal of this project is to implement the most common used technologies to serve as a starter template for any new project built with .NET

## Give a Star! :star:
If you like the project or if this helped you, please give a star :raised_hands:

## Built With

* [.NET 5](https://github.com/dotnet/core)
* [efcore](https://github.com/dotnet/efcore)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://github.com/FluentValidation/FluentValidation)
* [Swashbuckle](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
* [docker-compose](https://github.com/docker/compose)
* [bcrypt](https://github.com/BcryptNet/bcrypt.net)
* [MailKit](https://github.com/jstedfast/MailKit)

## Architecture

* Clean Architecture
* SOLID
* Domain Events
* Domain Notification
* Validation
* CQRS
* Unit of Work
* Repository

## Getting Started

To get a local copy up and running follow these simple steps.

### Prerequisites

* [Docker](https://docs.docker.com/get-docker/)

### Installation

1. Fill the missing SMTP config in `appsettings.json`
2. Run `make up` or `docker-compose up`
3. Go to `http://localhost:5000/docs`
