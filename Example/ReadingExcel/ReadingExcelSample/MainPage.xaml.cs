using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Lite.ExcelLibrary.SpreadSheet;

namespace ReadingExcelSample
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            this.bsyImport.IsBusy = true;
            //To access file system we need to use Silverlight file dialogs
            OpenFileDialog oFile = new OpenFileDialog();
            // .xls filter specified to select only .xls file.
            oFile.Filter = "Excel (*.xls)|*.xls";

            if (oFile.ShowDialog() == true)
            {
                // Get the stream of the selected file
                FileStream fs = oFile.File.OpenRead();
                // Simply call the Open method of Workbook and you are done
                Workbook book = Workbook.Open(fs);
                // All of the worksheet will be populated with data 
                // currently we will read only first for this sample
                Worksheet sheet = book.Worksheets[0];
               
                /// itrating through worksheet object to get values
                /// Worksheet.Cells.FirstRowIndex tells the First row index of data
                /// Worksheet.Cells.LastRowIndex tells the last of data
                /// Worksheet.Cells.FirstColIndex has first index of column value
                /// Worksheet.Cells.LastColIndex has last index of column
                /// So itrating using these properties will traverse all data of the sheet
                /// 
                for (int i = sheet.Cells.FirstRowIndex; i < sheet.Cells.LastRowIndex; i++)
                {
                    for (int j = sheet.Cells.FirstColIndex; j < sheet.Cells.LastColIndex; j++)
                    {
                        /// value of each cell is separated by a coma 
                        this.textBox1.Text += sheet.Cells[i, j].StringValue;
                        this.textBox1.Text += ",";

                    }
                    // New row will be displayed on next line
                    this.textBox1.Text += Environment.NewLine;
                }

                
            }
            this.bsyImport.IsBusy = false;
        }
    }
}
