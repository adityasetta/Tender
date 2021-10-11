CREATE PROCEDURE [dbo].[usp_UpdateTender]
    @userId VARCHAR(50),
    @body XML,
    @errorCode INT OUT, 
    @errorMessage VARCHAR(512) OUT
AS
BEGIN
    SET XACT_ABORT ON

    BEGIN TRY  
        BEGIN TRAN;
        
        DECLARE 
            @hDoc INT;

        EXEC sp_xml_preparedocument @hDoc OUTPUT, @body;
        
        DECLARE @TempUpdateTender TABLE(
            TenderId BIGINT,
            ContractNo VARCHAR(50),
            TenderName VARCHAR(50),
            ReleaseDate DATETIME,
            ClosingDate DATETIME,
            Description VARCHAR(5000)
        );

        INSERT INTO @TempUpdateTender
        SELECT *
        FROM OPENXML(@hDoc, 'UpdateTenderRequest')
        WITH (
            TenderId BIGINT 'TenderId',
            ContractNo VARCHAR(50) 'ContractNo',
            TenderName VARCHAR(50) 'TenderName',
            ReleaseDate DATETIME 'ReleaseDate',
            ClosingDate DATETIME 'ClosingDate',
            Description VARCHAR(5000) 'Description'
        );

        EXEC sp_xml_removedocument @hDoc;

        UPDATE trg
        SET trg.ContractNo = src.ContractNo,
        trg.TenderName = src.TenderName,
        trg.ReleaseDate = src.ReleaseDate,
        trg.ClosingDate = src.ClosingDate,
        trg.Description = src.Description
        FROM Tender trg
        INNER JOIN @TempUpdateTender src ON trg.TenderId = src.TenderId

        COMMIT TRAN

        SET @errorCode = 1;
        SET @errorMessage = '';
    END TRY  
    BEGIN CATCH  
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION

        SET @errorCode = ERROR_NUMBER(); 
        SET @errorMessage = ERROR_MESSAGE();
    END CATCH
END