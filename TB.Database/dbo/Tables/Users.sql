CREATE TABLE [dbo].[Users] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [UserName]     VARCHAR (50) NOT NULL,
    [PasswordHash] TEXT         NOT NULL,
    [EmailAddress] VARCHAR (50) NOT NULL,
    [PhoneNumber]  VARCHAR (10) NOT NULL,
    [Rating]       INT          NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

