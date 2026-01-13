CREATE TABLE [dbo].[Employees] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX)  NOT NULL,
    [Position] NVARCHAR (MAX)  NOT NULL,
    [Salary]   DECIMAL (18, 2) NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC)
);

