CREATE TABLE [dbo].[Positions] (
    [Id]    INT          NOT NULL,
    [Name]  VARCHAR (50) NOT NULL,
    [Price] NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_Positions] PRIMARY KEY CLUSTERED ([Id] ASC)
);

