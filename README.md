# Netcore-identity-sample
An implementation of .Net Core Identity, Authentication, Authorization with JWT.

- MySQL Database
- Dotnet UserSecrets to protect sensitive information in development environment.
- Swagger for Api Documentation

* For the purpose of this test project, I didn´t add unit test or created a complex architecture.
* If you want to debug the project you can execute it after the ```docker-compose up``` command, the secrets are hardcoded ;however,
  it is not recommended to store secrets in this way, we should use tools like AWS Secret Manager or Azure Vault instead.

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
