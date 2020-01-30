# QService

A micro service for queuing messages and distribution of the message. This is developed using **.Net Core 3.0**, **Docker**, and **RabbitMQ**. Most of the architecture and code is based directly on the article published in <https://medium.com/trimble-maps-engineering-blog/getting-started-with-net-core-docker-and-rabbitmq-part-1-a62601e784bb>.

This consists of 2 applications:

- Publisher Web API
- RabbitMQ Queue Service

Each of these services are run in **Docker** container and are decoupled. While any queue service can be used, initially RabbintMQ is used because of it popularity, ease of use, and availability. The 2 applications can be deployed using Docker Compose.

1. Publisher Web API
    The publisher web api is developed in **ASP.NET Core 3.0**. It provides RESTful endpoints that can be used to put messages in the queue. As a multi-platform microservice, this application can be deployed using docker.

    The purpose of this publisher api is to provide a single interface for any consumer to place messages in a queue. Initially there is only RabbitMQ being used but this could be enhanced so that adapters for other queue services (SQS in AWS or Azure Queues) can be developed. So this publisher api service can be adapted for multiple queue service and handle multiple clients targeting different queue services.

    Furthermore, Publisher API endpoints can be defined for specialized tasks such as sending email, running report, parsing logs etc.

    The publisher application will also need to provide a way for the consumers to retrieve response when applicable. RabbitMQ supports this via Remote Procedure Call (RPC) where each client has its own callback queue. Clients can use correlationid to match the response to the original request.

2. RabbitMQ Queue Service
