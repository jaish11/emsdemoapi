create proc GetEmployeeById
@Id int
as
begin
	select * from  Employees where Id=@Id
end