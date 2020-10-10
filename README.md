# AuditElasticsearchExample

Track database update entity and push to Elasticsearch when context save changes


What configurations includes in Dockerfile?

* auditexample-web netcore mvc project
* Elastic search & Kibana 
* Mssql Server 

Run with bellow Docker commands .

```
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml build

```

```

docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d

```
