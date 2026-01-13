create proc sp_DeleteStateByID
(@Id int)
as
begin
    Delete From States where Id= @Id
end