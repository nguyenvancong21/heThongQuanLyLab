CREATE TABLE [dbo].[Account] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [UserName]    NVARCHAR (MAX) NOT NULL,
    [Password]    NVARCHAR (MAX) NOT NULL,
    [AccountType] NVARCHAR (MAX) NOT NULL,
    [AccountVar]  NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

