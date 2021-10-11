CREATE PROCEDURE [dbo].[usp_PostTender]
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
        
        DECLARE @TempPostTender TABLE(
            ContractNo VARCHAR(50),
            TenderName VARCHAR(50),
            ReleaseDate DATETIME,
            ClosingDate DATETIME,
            Description VARCHAR(5000)
        );

        INSERT INTO @TempPostTender
        SELECT *
        FROM OPENXML(@hDoc, 'PostTenderRequest')
        WITH (
            ContractNo VARCHAR(50) 'ContractNo',
            TenderName VARCHAR(50) 'TenderName',
            ReleaseDate DATETIME 'ReleaseDate',
            ClosingDate DATETIME 'ClosingDate',
            Description VARCHAR(5000) 'Description'
        );

        EXEC sp_xml_removedocument @hDoc;

        INSERT INTO Tender(ContractNo, TenderName, ReleaseDate, ClosingDate, Description, CreatorId)
        SELECT ContractNo, TenderName, ReleaseDate, ClosingDate, Description, @userId
        FROM @TempPostTender

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