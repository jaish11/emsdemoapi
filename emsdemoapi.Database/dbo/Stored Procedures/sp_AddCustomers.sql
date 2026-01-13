create proc sp_AddCustomers
(@Name nvarChar(max),@Email nvarchar(max),@Mobile nvarchar(max),@Image nvarchar(max))
as
begin
    insert into Customers Values(@Name,@Email,@Mobile,@Image)
end