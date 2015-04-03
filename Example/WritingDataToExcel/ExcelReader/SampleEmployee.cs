using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace LiteExcelReader
{
    public class Friend : INotifyPropertyChanged
    {

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private int _FriendID;
        private string _FirstName;
        private string _LastName;        
        private string _Date;
        private string _Address;


        public int FriendID
        {
            get { return this._FriendID; }
            set
            {
                this._FriendID = value;
            }
        }


        public string FirstName
        {
            get { return this._FirstName; }
            set
            {
                this._FirstName = value;
            }
        }

        public string LastName
        {
            get { return this._LastName; }
            set
            {
                this._LastName = value;
            }
        }
        

        public string MDate
        {
            get { return this._Date; }
            set
            {
                this._Date = value;
            }
        }

        public string Address
        {
            get { return this._Address; }
            set
            {
                this._Address = value;
            }
        }

    }
}