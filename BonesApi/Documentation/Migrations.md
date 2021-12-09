## Migration creation

- If you want to update the database after you create a new table or edit something in a table:

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