create proc sp_DeleteDistrictsByID
(@Id int)
as
begin
    Delete From Districts where Id= @Id
end