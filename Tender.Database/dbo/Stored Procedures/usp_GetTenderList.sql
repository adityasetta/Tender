CREATE PROCEDURE [dbo].[usp_GetTenderList]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TenderId,
    ContractNo,
    TenderName,
    ReleaseDate,
    ClosingDate
    FROM Tender;
END