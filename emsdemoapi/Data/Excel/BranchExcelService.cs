using ClosedXML.Excel;
using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;

namespace emsdemoapi.Data.Excel
{
    public class BranchExcelService
    {
        public readonly IGeneric<Branch> _branchGeneric;
        public BranchExcelService(IGeneric<Branch> branchGeneric)
        {
            _branchGeneric = branchGeneric;
        }
        //Export: Generate Excel file from DB records
        public async Task<byte[]> ExportBranchesToExcelAsync()
        {
            var branches = await _branchGeneric.GetAllAsync();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Branches");
                // Adding Headers
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Code";
                int row = 2;
                foreach (var branch in branches)
                {
                    worksheet.Cell(row, 1).Value = branch.Id;
                    worksheet.Cell(row, 2).Value = branch.Name;
                    worksheet.Cell(row, 3).Value = branch.Code;
                    row++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
        //Import : Read Excel file and insert into DB
        public async Task ImportBranchesFromExcelAsync(Stream excelStream)
        {
            using (var workbook = new XLWorkbook(excelStream))
            {
                var worksheet = workbook.Worksheet("Branches");
                // Start reading from row 2 (skip headers)
                var rows = worksheet.RowsUsed().Skip(1);
                foreach (var row in rows)
                {
                    var branch = new Branch
                    {
                        Name = row.Cell(2).GetString(),
                        Code = row.Cell(3).GetString()
                    };
                    await _branchGeneric.AddAsync(branch);
                }
                await _branchGeneric.SaveAsync();
            }
        }
    }
}
