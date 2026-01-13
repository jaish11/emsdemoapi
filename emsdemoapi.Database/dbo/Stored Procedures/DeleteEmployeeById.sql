create proc DeleteEmployeeById
(@Id int)
as 
begin
	delete from Employees where Id = @Id
end