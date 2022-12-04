CREATE TABLE [dbo].[Notification] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [Title]   NVARCHAR (MAX) NOT NULL,
    [Link]    NVARCHAR (MAX) NOT NULL,
    [Date]    NVARCHAR (MAX) NOT NULL,
    [Unit]    NVARCHAR (MAX) NOT NULL,
    [Content] NVARCHAR (MAX) NULL,
    [Inner] BIT NULL DEFAULT 1,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

