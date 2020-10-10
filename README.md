# AuditElasticsearchExample

### Track database "updated entity" and push to Elasticsearch when context save changes.


## What configurations includes in Dockerfile?


- auditexample-web netcore mvc project
- Elastic search & Kibana 
- Mssql Server 

Run with bellow Docker commands:

```powershell
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml build
```

```powershell
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d
```


Kibana Url : http://localhost:5601 
MVC Web UI : http://localhost:5007
