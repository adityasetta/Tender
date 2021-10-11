CREATE PROCEDURE [dbo].[usp_GetTenderDetail]
    @tenderId	bigint
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TenderId,
    ContractNo,
    TenderName,
    ReleaseDate,
    ClosingDate,
    Description,
    CreatorId
    FROM Tender
    WHERE TenderId = @tenderId;
END