CREATE TABLE [dbo].[Orders] (
    [Id]             INT  NOT NULL,
    [ClientId]       INT  NOT NULL,
    [OrderDate]      DATE NOT NULL,
    [CompletionDate] DATE NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Orders_Clients] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Clients] ([Id])
);

