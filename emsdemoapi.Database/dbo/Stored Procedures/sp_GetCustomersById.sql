create proc sp_GetCustomersById
@Id int
as
begin
    select * from Customers where Id =@Id
end