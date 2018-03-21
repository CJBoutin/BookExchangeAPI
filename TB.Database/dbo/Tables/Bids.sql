CREATE TABLE [dbo].[Bids] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [UserId]        INT NOT NULL,
    [TransactionId] INT NOT NULL,
    [ProposedPrice] INT NOT NULL,
    CONSTRAINT [PK_Bids] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Bids_Transaction] FOREIGN KEY ([TransactionId]) REFERENCES [dbo].[Transactions] ([Id]),
    CONSTRAINT [FK_Bids_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

