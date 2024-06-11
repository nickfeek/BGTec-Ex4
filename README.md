### Ex4 Azure Blob Storage File Service (Web API) ###

**Assumptions.**

1. Images are posted to the "data/upload" endpoint
2. Image data is retreived from the "/data/" endpoint
3. Data is searched by date (not datetime)
4. Images need to be made unique


**Issues.**

1. I used Sqlite for the database. Obviously, this has consequences to the solution. Pricipally because there are no Date or Time types.
2. There are no unit tests.
3. Database fields are unvalidated TODO


**Notes.**

1. I used azurite for Azure Blob Storage emulation. Remember to start azurite with (azurite --location = ./Azurite in the project folder)
2. I used Sqlite for the database.
3. You'll need a tool like Postman to view the API (I dont have swagger set up).


**Endpoints.**

----

**http://localhost:5013/api/data/upload - POST**

Post a file using the "image" key.

----

**http://localhost:5013/api/data - GET**

e.g. http:/localhost:5013/api/data?start_date=07/05/2024&end_date=06/07/2024

Returns data for all uploaded files between the start and end date inclusive.

e.g 

```json
{
	"id": 27,
	"filename": "beachhouse.png",
	"size": 1727,
	"contentType": "image/png",
	"filenameExtension": ".png",
	"timestampProcessed": "2024-06-10T07:14:46",
	"filePath": "http://127.0.0.1:10000/devstoreaccount1/files/beachhouse-9ac1c505-8500-47a8-84f5-b5d82a53e02e.png"
}
```

----

**http://localhost:5013/api/data/files - GET**

Returns a list of uploaded files from the actual file system.

----

**http://localhost:5013/api/data/info - GET**

Returns "This is a Azure Blob Storage file system.".

