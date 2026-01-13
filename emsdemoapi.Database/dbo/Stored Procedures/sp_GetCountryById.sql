CREATE PROCEDURE sp_GetCountryById  
    @Id int  
AS   
BEGIN   
    SELECT * FROM Countries where Id = @Id  
END