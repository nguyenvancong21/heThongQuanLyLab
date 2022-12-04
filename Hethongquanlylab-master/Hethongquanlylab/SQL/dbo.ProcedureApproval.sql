CREATE TABLE [dbo].[ProcedureApproval] (
    [ID]              INT            IDENTITY (1, 1) NOT NULL,
    [SubID]           NVARCHAR (50)  NOT NULL,
    [Name]            NVARCHAR (MAX) NOT NULL,
    [Unit]            NVARCHAR (50)  NOT NULL,
    [ApprovalWaiting] NVARCHAR (50)  DEFAULT ('Ban Ði?u Hành') NOT NULL,
    [Senddate]        NVARCHAR (50)  NULL,
    [Content]         NVARCHAR (MAX) NULL,
    [Link]            NVARCHAR (MAX) NULL,
    [V1]              BIT            DEFAULT ((0)) NULL,
    [V2]              BIT            DEFAULT ((0)) NULL,
    [V3]              BIT            DEFAULT ((0)) NULL,
    [BdhReply]        NVARCHAR (MAX) DEFAULT ('Chua có ph?n h?i') NULL,
    [BcvReply]        NVARCHAR (MAX) DEFAULT ('Chua có ph?n h?i') NULL,
    [NSLReply]        NVARCHAR (MAX) DEFAULT ('Chua có ph?n h?i') NULL,
    [NDSLReply]        NVARCHAR (MAX) DEFAULT ('Chua có ph?n h?i') NULL,
    [Status]          NVARCHAR (50)  DEFAULT ('Chua duy?t') NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

