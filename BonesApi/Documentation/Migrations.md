## Migration creation

- If you want to do anything of the following:
    - Create the database for the first time 
    - Update the database after you create a new table.
    - Update the database after you edit something in a table.

1- First create a migration for it using the following command

```bash
dotnet ef migrations add migrationName
```

Where `migrationName` can be anything you want.

2- Update the database (Apply changes) using the following command

```bash
dotnet ef database update
```

This applies all pending migrations to the database file.