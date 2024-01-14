# Netcore-identity-sample
An implementation of .Net Core Identity, Authentication, Authorization with JWT.

- MySQL Database
- Swagger for Api Documentation
- Docker
- Logging with ElasticSearch and Kibana
- Message Broker with RabbitMQ
- For the purpose of this test project, I didn't add unit tests or created a complex architecture.
- If you want to debug the project you can execute it after the ```docker-compose up``` command, the secrets are hardcoded ;however,
  it's not recommended to store secrets in this way, we should use tools like AWS Secret Manager or Azure Vault instead.

To start the project use docker-compose
```
docker-compose up
```
```
http://localhost:8080/swagger/index.html
```

If you want to see the MySql Database using Workbench

```
Host: 127.0.0.1
Port: 7000
Username: my_database_username
Password: my_database_password
Default Schema: myuserapi
```
# Logging with ElasticSearch and Kibana

```http://localhost:8080/swagger/index.html```
![image](https://github.com/Geraldoab/netcore-identity-sample/assets/3846304/da7b1acb-8554-4cf6-9145-114c3e2cee7a)

``` http://localhost:5601/app/home#/ ```

![image](https://github.com/Geraldoab/netcore-identity-sample/assets/3846304/ff444ef2-d21b-4004-a27b-bbff8138850e)

# Message Broker with RabbitMQ

### How to setup your RabbitMQ:

#### Exchange
```
http://localhost:15672/#/exchanges
Exchange name: userapi-service-exchange
Type: topic
```
#### Queue
```
http://localhost:15672/#/queues
Queue name: userapi-email-service-queue
```
#### Binding
```
From exchange: userapi-service-exchange
Routing key: userapi-routing-key
```

```http://localhost:8080/swagger/index.html```
![image](https://github.com/Geraldoab/netcore-identity-sample/assets/3846304/e577028e-442f-4dd4-90aa-675bc8621283)

```http://localhost:15672/#/queues/%2F/userapi-email-service-queue```
![image](https://github.com/Geraldoab/netcore-identity-sample/assets/3846304/597545d2-864b-4f8d-affa-47d2f7fbb1e6)

# Hosted Service - Message Consumer

[EmailMessageSubscriber.cs](https://github.com/Geraldoab/netcore-identity-sample/blob/main/EmailMessageConsumer/EmailMessageSubscriber.cs)

![image](https://github.com/Geraldoab/netcore-identity-sample/assets/3846304/c66c8f65-8f66-4f86-9493-920422ae2e8b)

## My DockerHub

https://hub.docker.com/repositories/geraldoab
