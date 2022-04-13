# AzureFunctionsBlobStorage
In this example I am using Azure Functions to fetch and read files from Azure Blob Storage using Azure Functions. 

## Blob Trigger
A blob trigger activates when a blob is uploaded to an container. Here i use ``Stream`` as the trigger attribute. In reading a file the trigger is used as a input binding, but when uploading a file it used as the output binding. The Connectionstring cane easily be found int he local-settings.json file, which is created with the Blob trigger function.
```csharp
[Blob("{BLOB-FILEPATH}",FileAccess.Write,Connection = "{CONNECTIONSTRING}")]
```

## Http Trigger
When creating a file, I've used an Http trigger as it's input binding. When uplaoding text to a new file, you can use ``Textwriter`` as the trigger attribute. Otherwise using ``Stream`` is the most logical to use.
```csharp
[HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
```
