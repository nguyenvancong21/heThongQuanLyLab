CREATE TABLE [dbo].[Member] (
    [Key]            INT            IDENTITY (1, 1) NOT NULL,
    [LabID]          NCHAR (8)      NOT NULL,
    [Name]           NVARCHAR (MAX) NOT NULL,
    [Sex]            NCHAR (10)     NULL,
    [Avt]            NVARCHAR (MAX) DEFAULT ('default.jpg') NULL,
    [Birthday]       NVARCHAR (50)  NULL,
    [Gen]            NCHAR (10)     NULL,
    [Specialization] NVARCHAR (MAX) NULL,
    [University]     NVARCHAR (MAX) NULL,
    [Phone]          NVARCHAR (50)  NULL,
    [Email]          NVARCHAR (MAX) NOT NULL,
    [Address]        NVARCHAR (MAX) NULL,
    [Unit]           NVARCHAR (MAX) DEFAULT ('Không') NULL,
    [Position]       NVARCHAR (MAX) DEFAULT ('Không') NULL,
    [IsLT]           BIT            DEFAULT ((0)) NULL,
    [IsPassPTBT]     BIT            DEFAULT ((0)) NULL,
    [Status]         NUMERIC (18)   DEFAULT ((1)) NULL,
    [Assessment]     NVARCHAR (MAX) DEFAULT ('Không') NULL,
    PRIMARY KEY CLUSTERED ([Key] ASC)
);

