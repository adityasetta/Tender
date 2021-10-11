CREATE TABLE [dbo].[TenderHistory] (
    [TenderId]     BIGINT         NOT NULL,
    [ContractNo]   VARCHAR (50)   NOT NULL,
    [TenderName]   VARCHAR (50)   NOT NULL,
    [ReleaseDate]  DATETIME       NOT NULL,
    [ClosingDate]  DATETIME       NOT NULL,
    [Description]  VARCHAR (5000) NOT NULL,
    [CreatorId]    VARCHAR (50)   NOT NULL,
    [CreatedDate]  DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [ArchivedBy]   VARCHAR (50)   NOT NULL,
    [ArchivedDate] DATETIME       DEFAULT (getutcdate()) NOT NULL,
    [Reason]       VARCHAR (10)   NOT NULL,
    PRIMARY KEY CLUSTERED ([TenderId] ASC)
);

