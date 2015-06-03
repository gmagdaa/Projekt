using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Forms;
using System.Threading;



namespace Duties.Model
{
    class Print
    {

        string path;
        

        public void print()
        {
            select_path();

            Duty d = new Duty();
            List<string[]> staff_list = d.SelectDutyForPrint();

            
            for (int i = 0; i < staff_list.Count; i++)
            {

               //Thread thr = new Thread(() =>
               //{

                    printDuty(staff_list[i], i);
              // });
              // thr.Start();
       
            }


        }

       
        public void printDuty(string[] staff, int i)
        {
            string duty_day_string;
            using (var doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35))
            {

                try
                {
                    
                    using (var wri = PdfWriter.GetInstance(doc, new FileStream(path + "\\Dyżur " + i + ".pdf", FileMode.Create)))
                    //using (var wri = PdfWriter.GetInstance(doc, new FileStream(path + "\\Dyżur " + staff[2] + " " + staff[1] +" " + staff[3] +" " + staff[4] + " " + staff[5] + ".pdf", FileMode.Create)))
                    {

                        doc.Open();

                        Paragraph new_line = new Paragraph(Environment.NewLine, FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.CP1250, 20));
                        doc.Add(new_line);
                        Paragraph degree = new Paragraph(staff[0], FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.CP1250, 20));
                        doc.Add(degree);
                        Paragraph first_name = new Paragraph(staff[1], FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.CP1250, 20));
                        doc.Add(first_name);
                        Paragraph last_name = new Paragraph(staff[2], FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.CP1250, 20));
                        doc.Add(last_name);

                        doc.Add(new_line);

                        duty_day_string = deconvert(staff[3]);
                        Paragraph duty_day = new Paragraph(duty_day_string, FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.CP1250, 10));
                        doc.Add(duty_day);
                        Paragraph time_from = new Paragraph(staff[4], FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.CP1250, 10));
                        doc.Add(time_from);
                        Paragraph dash = new Paragraph("-", FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.CP1250, 10));
                        doc.Add(dash);
                        Paragraph time_to = new Paragraph(staff[5], FontFactory.GetFont(FontFactory.TIMES_ROMAN, BaseFont.CP1250, 10));
                        doc.Add(time_to);
                        doc.Close();
                        
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

            }
        }
        public void select_path()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            path = dialog.SelectedPath;
        }

        public string deconvert(string duty_day)
        {
            string duty_day_string = "";
            if (duty_day == "1")
                duty_day_string = "Poniedziałek";
            if (duty_day == "2")
                duty_day_string = "Wtorek";
            if (duty_day == "3")
                duty_day_string = "Środa";
            if (duty_day == "4")
                duty_day_string = "Czwartek";
            if (duty_day == "5")
                duty_day_string = "Piątek";
            return duty_day_string;
        }

    }
}
