# bulk-copier

C# Job to bulk copy data from one table to another

The following lines will need to be added to their own respective files

Add this to a new file called appSettings.config at the folder root
```xml
<appSettings>
  <add key="sourceQuery" value="Enter Query Here"/>
  <add key="destinationTable" value="Enter Destination Table Name Here"/>
</appSettings>
```

Add this to a new file called connectionStrings.config at the folder root
```xml
<connectionStrings>
  <add name="DESTINATION" connectionString="Enter Destination Connection String Here" providerName="System.Data.SqlClient"/>
  <add name="SOURCE" connectionString="Enter Source Connection String Here" providerName="System.Data.SqlClient"/>
</connectionStrings>
```

Make the necessary updates to these configuration variables as needed
