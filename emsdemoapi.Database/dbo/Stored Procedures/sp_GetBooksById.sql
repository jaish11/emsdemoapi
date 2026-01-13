create proc sp_GetBooksById
@Id int
as
begin
    select * from Books where Id =@Id
end