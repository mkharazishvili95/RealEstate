BEGIN TRANSACTION;
GO

ALTER TABLE [Apartments] ADD [ApartmentDealType] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Apartments] ADD [ApartmentState] int NULL;
GO

ALTER TABLE [Apartments] ADD [ApartmentType] int NOT NULL DEFAULT 0;
GO

ALTER TABLE [Apartments] ADD [BuildingStatus] int NULL;
GO

ALTER TABLE [Apartments] ADD [CityId] int NULL;
GO

ALTER TABLE [Apartments] ADD [DistrictId] int NULL;
GO

ALTER TABLE [Apartments] ADD [StreetId] int NULL;
GO

ALTER TABLE [Apartments] ADD [SubdistrictId] int NULL;
GO

CREATE TABLE [Files] (
    [Id] int NOT NULL IDENTITY,
    [ApartmentId] int NULL,
    [FileName] nvarchar(max) NULL,
    [FileContent] nvarchar(max) NULL,
    [FileType] int NULL,
    CONSTRAINT [PK_Files] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Tariffs] (
    [Id] int NOT NULL IDENTITY,
    [PaidService] int NULL,
    [Price] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_Tariffs] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250824185913_AddedFileEntityModel', N'6.0.0');
GO

COMMIT;
GO

