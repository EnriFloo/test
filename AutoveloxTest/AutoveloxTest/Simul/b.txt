per cambiare le autorizzazioni:
ALTER AUTHORIZATION ON DATABASE::Autovelox TO [sa];

--------------------

creare utente:
CREATE LOGIN cloud2325 
WITH PASSWORD = 'Its-2025', 
DEFAULT_DATABASE = [master];

-----------------------

USE Autovelox;
CREATE USER cloud2325 FOR LOGIN cloud2325;
ALTER ROLE db_owner ADD MEMBER cloud2325;

------------------------

Install-Package Microsoft.EntityFrameworkCore

Install-Package Microsoft.EntityFrameworkCore.SqlServer

Install-Package Microsoft.EntityFrameworkCore.Tools

Install-Package Microsoft.EntityFrameworkCore.Design

------------------------

Scaffold-DbContext "Server=.\SQLEXPRESS;Database=Autovelox;User Id=its;Password=Its-2025;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context AutoveloxContext -DataAnnotations

---------------------------

In classe DbContext sostituire il seguente metodo con la sintassi sottostante per risolvere il problema del #warning To protect potentially sensitive information in your connection string....

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

{

    IConfigurationRoot configuration = new ConfigurationBuilder()

        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)

        .AddJsonFile("appsettings.json")

        .Build();



    optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

}

-----------------------------------------------


