CREATE TABLE [dbo].[Training] (
    [ID]      INT            IDENTITY (1, 1) NOT NULL,
    [SubID]   NCHAR (10)     NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    [Speaker] NVARCHAR (MAX) NULL,
    [Link]    NVARCHAR (MAX) NOT NULL,
    [Date]    NVARCHAR (MAX) NOT NULL,
    [Unit]    NVARCHAR (MAX) NOT NULL,
    [Content] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

