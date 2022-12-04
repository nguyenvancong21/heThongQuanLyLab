CREATE TABLE [dbo].[Project] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (MAX) NOT NULL,
    [StartDay] NVARCHAR (MAX) NULL,
    [EndDay]   NVARCHAR (MAX) NULL,
    [Unit]     NVARCHAR (MAX) NULL,
    [Content]  NVARCHAR (MAX) NULL,
    [Type]     NVARCHAR (MAX) NULL,
    [Link]     NVARCHAR (MAX) NULL,
    [Member]   NVARCHAR (MAX) NULL,
    [Status]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

