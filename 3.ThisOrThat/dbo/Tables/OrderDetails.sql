CREATE TABLE [dbo].[OrderDetails] (
    [Id]         INT          NOT NULL,
    [OrderId]    INT          NOT NULL,
    [PositionId] INT          NOT NULL,
    [Amount]     NUMERIC (18) NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_OrderDetails_Orders] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Orders] ([Id]),
    CONSTRAINT [FK_OrderDetails_Positions] FOREIGN KEY ([PositionId]) REFERENCES [dbo].[Positions] ([Id])
);

