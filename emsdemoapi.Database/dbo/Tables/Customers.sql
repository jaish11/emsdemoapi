CREATE TABLE [dbo].[Customers] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [Email]  NVARCHAR (MAX) NOT NULL,
    [Mobile] NVARCHAR (MAX) NOT NULL,
    [Image]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC)
);

