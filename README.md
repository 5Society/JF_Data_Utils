# JF_Data_Utils

[JF_Data_Utils] is a opensource library to implement data layer using EF Code First

## Status
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=5Society_JF_Data_Utils&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=5Society_JF_Data_Utils)

## Overview
JF_Data_Utils provides a general aproach to data entities persintence. ([Implement the infrastructure persistence layer with Entity Framework Core](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/infrastructure-persistence-layer-implementation-entity-framework-core)) using

1. EF Code First
2. Unit of Work
3. Repository patron
4. Soft delete entity
5. Auditable entity

## Features
* Soft delete entity
* Auditable entity
* Unit Of Work
* Paging
* Controller extension

## Dependencies:
* Framework: .NET 7
* Microsoft.EntityFrameworkCore
* NUnit
* Coverlet

## Getting Started
To learn hoy to use the library , review the example project: API_JF_Data_Utils_Example

## API_JF_Data_Utils_Example
Web API project using .NET 7 : Example project using JF_Data_Utils.
This project aims to create a Rest API without authentication using the JF_Data_Utils library.
