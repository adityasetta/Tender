CREATE PROCEDURE [dbo].[usp_DeleteTender]
    @userId VARCHAR(50),
    @tenderId BIGINT,
    @errorCode INT OUT, 
    @errorMessage VARCHAR(512) OUT
AS
BEGIN
    SET XACT_ABORT ON

    BEGIN TRY  
        BEGIN TRAN;				

        -- Insert the deleted data to history table
        INSERT INTO TenderHistory(
        TenderId, 
        ContractNo, 
        TenderName,
        ReleaseDate,
        ClosingDate, 
        Description, 
        CreatorId, 
        CreatedDate, 
        ArchivedBy, 
        ArchivedDate, 
        Reason)
        SELECT 
        TenderId, 
        ContractNo, 
        TenderName,
        ReleaseDate,
        ClosingDate, 
        Description, 
        CreatorId, 
        CreatedDate,
        @userId,
        GETUTCDATE(),
        'deleted'
        FROM Tender
        WHERE TenderId = @tenderId;

        -- Delete data from transaction table, to optimize the performance
        DELETE FROM Tender
        WHERE TenderId = @tenderId;

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