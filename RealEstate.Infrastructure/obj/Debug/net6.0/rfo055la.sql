BEGIN TRANSACTION;
GO

ALTER TABLE [Files] ADD [DeleteDate] datetime2 NULL;
GO

ALTER TABLE [Files] ADD [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit);
GO

ALTER TABLE [Files] ADD [UploadDate] datetime2 NULL;
GO

ALTER TABLE [Files] ADD [UserId] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250824200649_ParametersIntoFile', N'6.0.0');
GO

COMMIT;
GO

