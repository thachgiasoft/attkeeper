﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using AttendanceKeeper.Data;
using Microsoft.Office.Interop;
using JBiometric.Entities;
using Enrollee = AttendanceKeeper.Data.Enrollee;

namespace AttendanceKeeper.Classes
{
    class UtilityClass
    {
        # region "image processing"
        public static byte[] ImageToByte(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static Image ByteToImage(byte[] byteArray)
        {
            MemoryStream ms = new MemoryStream(byteArray);
            Image image = Image.FromStream(ms);
            return image;
        }

        public static byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long lBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)lBytes);
            return data;
        }

        public static void GetImage(byte[] bData, PictureBox pics)
        {
            try
            {
                byte[] imageData = bData;
                Image newImage;
                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                {
                    ms.Write(imageData, 0, imageData.Length);
                    newImage = Image.FromStream(ms, true);
                }
                pics.Image = newImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void WaterMarkOnImage(PictureBox pictureBox1)
        {
            if (pictureBox1.Image != null)
            {
                //Create image.
                Image tmp = pictureBox1.Image;
                //Create graphics object for alteration.
                Graphics g = Graphics.FromImage(tmp);

                //Create string to draw. 
                String wmString = "Code Project";
                //Create font and brush.
                Font wmFont = new Font("Trebuchet MS", 10);
                SolidBrush wmBrush = new SolidBrush(Color.Black);
                //Create point for upper-left corner of drawing. 
                PointF wmPoint = new PointF(10.0F, 10.0F);
                //Draw string to image.
                g.DrawString(wmString, wmFont, wmBrush, wmPoint);
                //Load the new image to picturebox 
                pictureBox1.Image = tmp;
                //Release graphics object. 
                g.Dispose();
            }
        }

        #endregion

        # region "control utilities"
        public static void ClearControls(Form f)
        {
            foreach (Control c in f.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).Clear();
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)c).SelectedIndex = -1;
                    ((ComboBox)c).Text = "";
                }
                else if (c.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c.GetType() == typeof(DateTimePicker))
                {
                    ((DateTimePicker)c).Format = DateTimePickerFormat.Custom;
                    ((DateTimePicker)c).CustomFormat = " ";
                }
            }
        }

        public static void ClearControls(Panel p, bool o)
        {
            foreach (Control c in p.Controls)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).Clear();
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)c).SelectedIndex = -1;
                    ((ComboBox)c).Text = "";
                }
                else if (c.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c.GetType() == typeof(DateTimePicker))
                {
                    ((DateTimePicker)c).Format = DateTimePickerFormat.Custom;
                    ((DateTimePicker)c).CustomFormat = " ";
                }
            }
        }

        public static void ChangeColor(Control c, bool o)
        {
            if (o == true)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).BackColor = Color.LemonChiffon;
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)c).BackColor = Color.LemonChiffon;
                }
            }
            else
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).BackColor = Color.White;
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)c).BackColor = Color.White;
                }
            }
        }

        public static void ChangeColor(Control c, bool o, Color eColor, Color lColor)
        {
            if (o == true)
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).BackColor = eColor;
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)c).BackColor = eColor;
                }
            }
            else
            {
                if (c.GetType() == typeof(TextBox))
                {
                    ((TextBox)c).BackColor = lColor;
                }
                else if (c.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)c).BackColor = lColor;
                }
            }
        }

        #endregion

        #region "date utilities"
        public static List<string> FillMonth()
        {
            List<string> lMonth = new List<string>();
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            foreach (var item in months)
            {
                lMonth.Add(item);
            }
            return lMonth;
        }

        public static List<string> FillMonth(int iMonths)
        {
            List<string> lMonth = new List<string>();
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            foreach (var item in months)
            {
                lMonth.Add(item);
                if (lMonth.Count == iMonths)
                    return lMonth;
            }
            return lMonth;
        }

        public static Dictionary<int, string> FillMonthDic()
        {
            Dictionary<int, string> dMonth = new Dictionary<int, string>();
            dMonth.Add(1, "January");
            dMonth.Add(2, "February");
            dMonth.Add(3, "March");
            dMonth.Add(4, "April");
            dMonth.Add(5, "May");
            dMonth.Add(6, "June");
            dMonth.Add(7, "July");
            dMonth.Add(8, "August");
            dMonth.Add(9, "September");
            dMonth.Add(10, "October");
            dMonth.Add(11, "November");
            dMonth.Add(12, "December");
            return dMonth;
        }

        public static List<int> FillYear()
        {
            List<int> lYear = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                lYear.Add(DateTime.Now.Year - i);
            }
            return lYear;
        }

        public static List<int> FillDays()
        {
            List<int> lDays = new List<int>();
            for (int i = 0; i < 31; i++)
            {
                lDays.Add(i + 1);
            }
            return lDays;
        }

        public static List<int> FillHours()
        {
            List<int> lHours = new List<int>();
            for (int i = 0; i < 24; i++)
            {
                lHours.Add(i + 1);
            }
            return lHours;
        }

        public static List<int> FillMins()
        {
            List<int> lMins = new List<int>();
            for (int i = 0; i < 59; i++)
            {
                lMins.Add(i + 1);
            }
            return lMins;
        }

        public static List<int> FillSecs()
        {
            List<int> lSecs = new List<int>();
            for (int i = 0; i < 59; i++)
            {
                lSecs.Add(i + 1);
            }
            return lSecs;
        }
        #endregion

        #region "BackUp/Restore"

        #endregion  

        #region "others"
        public static DialogResult GetDeleteDialog(string className)
        {
            DialogResult dResult = DialogResult.No;
            dResult = MessageBox.Show(string.Format("You are about to delete a(n) {0} record, continue?", className.ToUpper()), "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            return dResult;
        }

        public static void GetMessageBox(int iSaveMessage)
        {
            if (iSaveMessage > 0)
            {
                MessageBox.Show("Record saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (iSaveMessage == 0)
            {
                MessageBox.Show("Record not saved.", "Not Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void StatusBarText(ToolStripStatusLabel statusText, int iDisplayMode)
        {
            switch (iDisplayMode)
            {
                case 1:
                    statusText.Text = "Editing...";
                    break;
                case 2:
                    statusText.Text = "Done Editing...";
                    break;
                case 3:
                    statusText.Text = "Record Saved...";
                    Thread.Sleep(100);
                    break;
                case 4:
                    statusText.Text = "An Error Occured. Record was not Saved...";
                    break;
                case 5:
                    statusText.Text = "System Ready...";
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region "Excel Import"
        public static List<Enrollee> ImportEnrollees(string path)
        {
            var listEnrollee = new List<Enrollee>();
            var app = new Microsoft.Office.Interop.Excel.ApplicationClass(); 
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0); 
            var workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet; 
            
            int index = 0; 
            object rowIndex = 2;

            try
            {
                while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)
                {
                    rowIndex = 2 + index;
                    var enrollee = new Enrollee();
                    enrollee.EnrolleeNo = Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2);
                    enrollee.EnrolleeIdNo = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2);
                    enrollee.LastName = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
                    enrollee.FirstName = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2);
                    enrollee.MiddleName = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 4]).Value2);
                    enrollee.Sex = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 5]).Value2);
                    enrollee.IsActive = true;
                    listEnrollee.Add(enrollee);
                    index++;
                } 
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error - " + ex.Message);
                //throw;
            }
            app.Workbooks.Close(); 
            return listEnrollee;
        }

        public static List<AttLog> ImportLogsFromExcel(String path)
        {
            var listLogs = new List<AttLog>();
            var app = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            var workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;

            int index = 0;
            object rowIndex = 2;

            try
            {
                while (((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2 != null)
                {
                    rowIndex = 2 + index;
                    var log = new AttLog();
                    log.MachineNo = 1;
                    if (Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 1]).Value2) <=
                        0) continue;
                    log.EnrolleeNo =
                        Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 1]).Value2);
                    log.EnrollNumber =
                        Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 1]).Value2);
                    log.IYear =
                        Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 2]).Value2);
                    log.IMonth =
                        Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 3]).Value2);
                    log.IDay =
                        Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 4]).Value2);
                    log.IHour =
                        Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 5]).Value2);
                    log.IMin =
                        Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 6]).Value2);
                    log.IMinute =
                        Convert.ToInt32(((Microsoft.Office.Interop.Excel.Range) workSheet.Cells[rowIndex, 6]).Value2);
                    listLogs.Add(log);
                    index++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"Error - " + ex.Message);
                //throw;
            }
            app.Workbooks.Close();
            return listLogs;
        }

        #endregion

        #region "app.config"
        public static string GetIPAddress()
        {
            return System.Configuration.ConfigurationManager.AppSettings["IPAddress"];
        }

        public static int GetIPort()
        {
            return Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["IPort"]);
        }

        public static bool IsValidIP(string addr)
        {
            return Regex.IsMatch(addr, @"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$\b");
        } 

        public static string GetScreenType()
        {
            return System.Configuration.ConfigurationManager.AppSettings["ScreenType"];
        }

        public static string GetMachineSN()
        {
            return System.Configuration.ConfigurationManager.AppSettings["MachineSN"];
        }

        public static string GetMacKeyFinal()
        {
            //return System.Configuration.ConfigurationManager.AppSettings["MacKeyFinal-Roxas"];
            //return System.Configuration.ConfigurationManager.AppSettings["MacKeyFinal-RNF"];
            //return System.Configuration.ConfigurationManager.AppSettings["MacKeyFinal-Carmen"];
            return System.Configuration.ConfigurationManager.AppSettings["MacKeyFinal-Aleosan"];
        }

        public static string GetMacKeyTemp()
        {
            return System.Configuration.ConfigurationManager.AppSettings["MacKeyTemp"];
        }
        #endregion
    }
}
