# :classical_building: Library Management

## :question: Problem Statement

Design and Develop a Library Management API using .NET Core with the below features 

:standing_person: User Module:

1. Authentication [Users should log in to the application to view the list of books - No UI needed].
2. User can select a Book to Read [Favorites].
3. User can mark a book read [History].
4. User can write a review of the book [Once written the review cannot be changed].

:standing_woman: Admin Module:

1. Authentication [Admins should log in to the application to view the list of books - No UI needed]
2. Admin Should be able to List, Create, Update and Delete Books. 
3. Admin Should be able to View all reviews for a Book.

## :closed_book: Services

1. Identity Service
   * Uses [Idenity server 4](https://identityserver4.readthedocs.io/en/latest/)
2. Catalog Service 
   * Sent events via [RabbitMQ](https://www.rabbitmq.com/)
   * Store information In-Memory, just a matter of time to replace that with any RDDBMS
3. Search Service
    * Store information in [ElasticSearch](https://www.elastic.co/elasticsearch/)
    * Listens to events in [RabbitMQ](https://www.rabbitmq.com/)
4. Review Service :diamonds:
    * Store informaiton in [MongoDB](https://www.mongodb.com/try/download/community)
5. Personalization Service :diamonds:
    * Store informaiton in [MongoDB](https://www.mongodb.com/try/download/community)

:diamonds: = upcoming


## :tada: Setup

### :running_man: Setup Infra

```sh
   cd infra
   docker-compose up -d
```

### :running_man: How to run Identity Service 

```sh
   dotnet run --project src/Identity-Server/Identity-Server.csproj
```

### :running_man: How to run Catalog Service 

```sh
   dotnet run --project src/Catalog-Server/Catalog-Server.csproj
```

### :running_man: How to run Search Service 

```sh
   dotnet run --project src/Search-Server/Search-Server.csproj
```

### :toolbox: Postman

Import [postman collections](docs/Library-Management.postman_collection.json) inside docs folder for testing the services. It has the test scripts written,
it will be matter of clicking the send button only unless you want to change some values.