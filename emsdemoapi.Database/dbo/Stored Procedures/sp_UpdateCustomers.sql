create proc sp_UpdateCustomers
(@Id int,@Name nvarChar(max),@Email nvarchar(max),@Mobile nvarchar(max),@Image nvarchar(max))
as
begin
    update Customers set Name = @Name, Email=@Email,Mobile=@Mobile,Image=@Image where Id =@Id
end