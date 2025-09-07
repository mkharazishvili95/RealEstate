BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[UserFavoriteApartments]') AND [c].[name] = N'UserFavoriteApplicationId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [UserFavoriteApartments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [UserFavoriteApartments] DROP COLUMN [UserFavoriteApplicationId];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250907162235_RemovedUselessId', N'6.0.0');
GO

COMMIT;
GO

