create proc sp_DeleteBookByID
(@Id int)
as
begin
    Delete From Books where Id= @Id
end