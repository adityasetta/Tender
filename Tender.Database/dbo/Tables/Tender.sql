CREATE TABLE [dbo].[Tender]
(
    [TenderId] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [ContractNo] VARCHAR(50) NOT NULL, 
    [TenderName] VARCHAR(50) NOT NULL, 
    [ReleaseDate] DATETIME NOT NULL, 
    [ClosingDate] DATETIME NOT NULL, 
    [Description] VARCHAR(5000) NOT NULL,
    [CreatorId] VARCHAR(50) NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE()
)
