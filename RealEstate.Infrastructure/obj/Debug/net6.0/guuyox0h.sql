BEGIN TRANSACTION;
GO

ALTER TABLE [Files] ADD [MainImage] bit NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250906115714_AddedMainImageIntoFiles', N'6.0.0');
GO

COMMIT;
GO

