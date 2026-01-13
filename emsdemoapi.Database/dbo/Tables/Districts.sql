CREATE TABLE [dbo].[Districts] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (MAX) NOT NULL,
    [CountryId] INT            NOT NULL,
    [StateId]   INT            NOT NULL,
    CONSTRAINT [PK_Districts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

