create proc sp_DeleteCustomerByID
(@Id int)
as
begin
    Delete From Customers where Id= @Id
end