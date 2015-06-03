using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Duties.Base
{
    class ValidationObject : NotificationObject, IDataErrorInfo, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, Func<string, string>> _validators = new Dictionary<string, Func<string, string>>();

        private readonly Dictionary<string, string> _validationState = new Dictionary<string, string>();

        protected Dictionary<string, Func<string, string>> Validators
        {
            get { return _validators; }
        }

        #region IDataErrorInfo

        public string this[string columnName]
        {
            get
            {
                string result = Validators.ContainsKey(columnName) ? Validators[columnName](columnName) : null;
                _validationState[columnName] = result;
                return result;
            }
        }

        public string Error
        {
            get { return string.Concat(_validationState.Values.Where(v => v != null)); }
        }

        #endregion

        #region INotifyDataErrorInfo

        public IEnumerable GetErrors(string propertyName)
        {
            if (Validators.ContainsKey(propertyName))
                return new[] { Validators[propertyName](propertyName) };
            return Enumerable.Empty<object>();
        }

        public bool HasErrors
        {
            get { return _validationState.Values.Any(v => v != null); }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void RaiseErrorsChanged([CallerMemberName] string propertyName = null)
        {
            var handler = ErrorsChanged;
            if (handler != null)
                handler(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion

    }
}
