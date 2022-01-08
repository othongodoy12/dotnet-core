using System;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace pdf
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(PageSize.A4.Rotate());
                document.SetMargins(3, 3, 3, 3);                

                PdfWriter pdfWriter = PdfWriter.GetInstance(document, ms);

                document.Open();

                PdfPTable table = new PdfPTable(3);
                
                Font font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, 22);

                Paragraph column1 = new Paragraph("First Name", font);
                Paragraph column2 = new Paragraph("Last Name", font);
                Paragraph column3 = new Paragraph("Fiscal Identification", font);

                PdfPCell cell1 = new PdfPCell();
                PdfPCell cell2 = new PdfPCell();
                PdfPCell cell3 = new PdfPCell();

                cell1.AddElement(column1);
                cell2.AddElement(column2);
                cell3.AddElement(column3);

                table.AddCell(cell1);
                table.AddCell(cell2);
                table.AddCell(cell3);

                List<Employee> employees = Employee.GetEmployees();

                employees.ForEach(employee => 
                {
                    Phrase firstName = new Phrase(employee.FirstName);
                    PdfPCell firstNameCell = new PdfPCell(firstName);
                    table.AddCell(firstNameCell);

                    Phrase lastName = new Phrase(employee.LastName);
                    PdfPCell lastNameCell = new PdfPCell(lastName);
                    table.AddCell(lastNameCell);

                    Phrase fiscalIdentification = new Phrase(employee.FiscalIdentification);
                    PdfPCell fiscalIdentificationCell = new PdfPCell(fiscalIdentification);
                    table.AddCell(fiscalIdentificationCell);
                });

                document.Add(table);

                document.Close();

                byte[] documentFileBytes = ms.ToArray();

                Console.WriteLine(Convert.ToBase64String(documentFileBytes));
            }
        }        
    }

    class Employee
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FiscalIdentification { get; private set; }

        public static List<Employee> GetEmployees() =>
            new List<Employee>
            {
                new Employee { FirstName = "Othon", LastName = "Godoy", FiscalIdentification = "11111111111" },
                new Employee { FirstName = "Rafael", LastName = "Ferreira", FiscalIdentification = "22222222222" }                
            };
        
    }
}
