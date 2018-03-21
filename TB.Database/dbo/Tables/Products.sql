CREATE TABLE [dbo].[Products] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [ISBN]      NVARCHAR (MAX) NOT NULL,
    [Title]     NVARCHAR (MAX) NOT NULL,
    [Author]    NVARCHAR (MAX) NULL,
    [Publisher] NVARCHAR (MAX) NULL,
    [UserId]    INT            NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Products_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

