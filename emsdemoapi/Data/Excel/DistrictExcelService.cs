using ClosedXML.Excel;
using emsdemoapi.Data.Entities;
using emsdemoapi.Data.Interfaces;
using emsdemoapi.Data.Services;

namespace emsdemoapi.Data.Excel
{
    public class DistrictExcelService
    {
        public readonly IDistrict _district;

        public DistrictExcelService(IDistrict district)
        {
            _district = district;
        }

        public byte[] ExportDistrictsToExcel()
        {
            var districts = _district.GetAllDistricts();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Districts");

                // Header row
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "CountryId";
                worksheet.Cell(1, 4).Value = "StateId";

                int row = 2;
                foreach (var d in districts)
                {
                    worksheet.Cell(row, 1).Value = d.Id;
                    worksheet.Cell(row, 2).Value = d.Name;
                    worksheet.Cell(row, 3).Value = d.CountryId;
                    worksheet.Cell(row, 4).Value = d.StateId;
                    row++;
                }

                // Optional: Apply styling
                var headerRange = worksheet.Range("A1:D1");
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGray;
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }

        public void ImportDistrictsFromExcel(Stream excelStream)
        {
            using (var workbook = new XLWorkbook(excelStream))
            {
                //var worksheet = workbook.Worksheet("Districts");
                var worksheet = workbook.Worksheets.FirstOrDefault();
                if(worksheet == null)
                {
                    throw new Exception("No worksheet found in the Excel file.");
                }
                var rows = worksheet.RowsUsed().Skip(1); // Skip header row

                foreach (var row in rows)
                {
                    if (row.Cell(1).IsEmpty() && row.Cell(2).IsEmpty())
                        continue;
                    var district = new District
                    {
                        Name = row.Cell(2).GetString(),
                        CountryId = row.Cell(3).GetValue<int>(),
                        StateId = row.Cell(4).GetValue<int>()
                    };

                    _district.AddDistrcit(district);
                }
            }
        }
    }
}
