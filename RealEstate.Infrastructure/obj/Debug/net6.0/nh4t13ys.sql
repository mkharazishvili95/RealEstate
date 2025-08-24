BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Files].[FileContent]', N'FileUrl', N'COLUMN';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250824194028_AddFileUrlToFile', N'6.0.0');
GO

COMMIT;
GO

