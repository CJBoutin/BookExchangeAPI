CREATE TABLE [dbo].[Images] (
    [Id]        INT   IDENTITY (1, 1) NOT NULL,
    [ProductId] INT   NOT NULL,
    [ImageData] IMAGE NOT NULL,
    [UserId]    INT   NOT NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Images_Products] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Products] ([Id]),
    CONSTRAINT [FK_Images_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

