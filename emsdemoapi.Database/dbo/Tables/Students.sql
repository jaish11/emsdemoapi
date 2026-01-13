CREATE TABLE [dbo].[Students] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [Age]     INT            NOT NULL,
    [Email]   NVARCHAR (MAX) NOT NULL,
    [Phone]   NVARCHAR (MAX) NOT NULL,
    [City]    NVARCHAR (MAX) NOT NULL,
    [State]   NVARCHAR (MAX) NOT NULL,
    [Country] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED ([Id] ASC)
);

