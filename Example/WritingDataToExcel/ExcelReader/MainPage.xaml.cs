using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using  Lite.ExcelLibrary.CompoundDocumentFormat;
using  Lite.ExcelLibrary.BinaryFileFormat;
using  Lite.ExcelLibrary.SpreadSheet;

using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Collections;
using System.Reflection;

namespace LiteExcelReader
{
    public partial class MainPage : UserControl
    {
        
        public ObservableCollection<Friend> FriendsData = new ObservableCollection<Friend>();        

        public MainPage()
        {
            InitializeComponent();
            this.FriendsData.Add(new Friend { FriendID = 1, FirstName = "中文", LastName = "Khan", MDate = "15-10-1998", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 2, FirstName = "imran", LastName = "Khan", MDate = "15-10-1998", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 3, FirstName = "zahid", LastName = "Jalal", MDate = "15-10-2001", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 4, FirstName = "faisal", LastName = "fazil", MDate = "15-10-2001", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 5, FirstName = "Ali", LastName = "Irfan", MDate = "15-10-2001", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 6, FirstName = "Abubakar", LastName = "Dar", MDate = "15-10-2000", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 7, FirstName = "Ali", LastName = "Hamzah", MDate = "15-10-2010", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 8, FirstName = "Asif", LastName = "Kaleem", MDate = "15-10-2010", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 9, FirstName = "Usamah", LastName = "Bukhari", MDate = "15-10-2010", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 10, FirstName = "Waheed", LastName = "Tofee", MDate = "15-10-2004", Address = "Pakistan" });
            this.FriendsData.Add(new Friend { FriendID = 11, FirstName = "Khurram", LastName = "Pappu", MDate = "15-10-2004", Address = "Pakistan" });
            this.theGrid.ItemsSource = this.FriendsData;
        }

      

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            // open file dialog to select an export file.   
            SaveFileDialog sDialog = new SaveFileDialog();
            sDialog.Filter = "Excel Files(*.xls)|*.xls";

            if (sDialog.ShowDialog() == true)
            {

                // create an instance of excel workbook
                Workbook workbook = new Workbook();
                // create a worksheet object
                Worksheet worksheet = new Worksheet("Friends");

                Int16 ColumnCount = 0;
                Int16 RowCount = 0;

                //Writing Column Names 
                foreach (DataGridColumn dgcol in theGrid.Columns)
                {
                    worksheet.Cells[0, ColumnCount] = new Cell(dgcol.Header.ToString());
                    ColumnCount++;
                }

                //Extracting values from grid and writing to excell sheet
                //
                foreach (object data in theGrid.ItemsSource)
                {
                    ColumnCount = 0;
                    RowCount++;
                    foreach (DataGridColumn col in theGrid.Columns)
                    {

                        string strValue = "";
                        Binding objBinding = null;
                        if (col is DataGridBoundColumn)
                            objBinding = (col as DataGridBoundColumn).Binding;
                        if (col is DataGridTemplateColumn)
                        {
                            //This is a template column... let us see the underlying dependency object
                            DependencyObject objDO = (col as DataGridTemplateColumn).CellTemplate.LoadContent();
                            FrameworkElement oFE = (FrameworkElement)objDO;
                            FieldInfo oFI = oFE.GetType().GetField("TextProperty");
                            if (oFI != null)
                            {
                                if (oFI.GetValue(null) != null)
                                {
                                    if (oFE.GetBindingExpression((DependencyProperty)oFI.GetValue(null)) != null)
                                        objBinding = oFE.GetBindingExpression((DependencyProperty)oFI.GetValue(null)).ParentBinding;
                                }
                            }
                        }
                        if (objBinding != null)
                        {
                            if (objBinding.Path.Path != "")
                            {
                                PropertyInfo pi = data.GetType().GetProperty(objBinding.Path.Path);
                                if (pi != null) strValue = Convert.ToString(pi.GetValue(data, null));
                            }
                            if (objBinding.Converter != null)
                            {
                                if (strValue != "")
                                    strValue = objBinding.Converter.Convert(strValue, typeof(string), objBinding.ConverterParameter, objBinding.ConverterCulture).ToString();
                                //else
                                //    strValue = objBinding.Converter.Convert(data, typeof(string), objBinding.ConverterParameter, objBinding.ConverterCulture).ToString();
                            }
                        }
                        // writing extracted value in excell cell
                        worksheet.Cells[RowCount, ColumnCount] = new Cell(strValue);
                        ColumnCount++;
                    }


                }

                //add worksheet to workbook
                workbook.Worksheets.Add(worksheet);
                // get the selected file's stream
                Stream sFile = sDialog.OpenFile();
                workbook.Save(sFile);

            }

        }

    }
}
