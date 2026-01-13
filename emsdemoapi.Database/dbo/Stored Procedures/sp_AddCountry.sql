CREATE PROCEDURE sp_AddCountry   
    @Name NVARCHAR(150)  
AS   
BEGIN   
    INSERT INTO Countries VALUES (@Name);   
END