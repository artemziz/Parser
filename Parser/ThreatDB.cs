using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Excel =  Microsoft.Office.Interop.Excel;

namespace Parser
{
    [Serializable]
    class ThreatDB
    {
        public  List<Threat> threats = new List<Threat>();
        public List<List<Threat>> pages = new List<List<Threat>>();
        int count = 0;
        int itemsPerPage = 15;
        int lastPageCount = 0;
        
        public void CreatePages()
        {
            int pagesCount = threats.Count / itemsPerPage;
            if (threats.Count != pagesCount * itemsPerPage)
            {
                lastPageCount = threats.Count % itemsPerPage;
            }
            for(int i = 0; i < pagesCount; i++)
            {
                List<Threat> page = new List<Threat>();
                for(int j = 0; j < itemsPerPage; j++)
                {
                    page.Add(threats[count]);
                    count++;
                }
                pages.Add(page);
            }
            List<Threat> LastPage = new List<Threat>();
            for(int i = 0; i < lastPageCount; i++)
            {
                LastPage.Add(threats[count]);
                count++;
            }
            pages.Add(LastPage);
        }
        public void Parse(string path)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Workbooks.Open(path);
            int row = 3;
            Excel.Worksheet currentSheet = (Excel.Worksheet)excelApp.Workbooks[1].Worksheets[1];

            while (currentSheet.get_Range("A" + row).Value2 != null)
            {
                
                Threat threat = new Threat
                {
                    Id = Convert.ToInt32(currentSheet.get_Range("A" + row).Value2),
                    Name = currentSheet.get_Range("B" + row).Value2,
                    Description = currentSheet.get_Range("C" + row).Value2,
                    Source = currentSheet.get_Range("D" + row).Value2,
                    Subject = currentSheet.get_Range("E" + row).Value2,
                    Confidentiality = Convert.ToBoolean(currentSheet.get_Range("F" + row).Value2),
                    Integrity  = Convert.ToBoolean(currentSheet.get_Range("G" + row).Value2),
                    Availability = Convert.ToBoolean(currentSheet.get_Range("H" + row).Value2),
                };
                threats.Add(threat);
                row++;
            }
            excelApp.Quit();
        }
    }
}
