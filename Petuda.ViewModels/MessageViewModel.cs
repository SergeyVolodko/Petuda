using System;

namespace Petuda.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        #region Fields

        private string _title;
        private string _message;
        private bool _isDialog;

        #endregion

        #region Properties

        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title)
                    return;

                _title = value;
                NotifyPropertChanged("Title");
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                if (value == _message)
                    return;

                _message = value;
                NotifyPropertChanged("Message");
            }
        }

        public bool IsDialog
        {
            get { return _isDialog; }
            set
            {
                if (value == _isDialog)
                    return;

                _isDialog = value;
                NotifyPropertChanged("IsDialog");
            }
        }

        #endregion

        public MessageViewModel(string title, string message, bool isDialog)
        {
            this.Title = title;
            this.Message = message;
            this.IsDialog = isDialog;
        }
    }
}