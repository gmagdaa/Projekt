using Duties.Base;
using Duties.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Duties.ViewModel
{
    class EditDutyViewModel : ValidationObject
    {
        private int duty_id;
        public EditDutyViewModel(int id)
        {
            duty_id = id;
            loadDays();
            Duty d = new Duty();
            duty = d.SelectDuty(duty_id);
            _selectedDay = (string)duty[0]; 
            _hourFrom = (string)duty[1];
            _hourTo = (string)duty[2];

            Regex re = new Regex("[0-2][0-9]\\.[0-9][0-9]");
            //Validators.Add("DaySource", p => (string.IsNullOrEmpty(SelectedDay) || Int32.Parse(SelectedDay) == 0) ? "Godzina od nie może być pusta" : null);
            Validators.Add("HourFrom", p => (string.IsNullOrEmpty(HourFrom) || !re.IsMatch(HourFrom)) ? "Godzina od nie może być pusta" : null);
            Validators.Add("HourTo", p => (string.IsNullOrEmpty(HourTo) || !re.IsMatch(HourTo)) ? "Godzina do nie może być pusta" : null);
            //Validators.Add("HourTo", p => (string.IsNullOrEmpty(HourTo) || !re.IsMatch(HourTo)) ? "Godzina do nie może być pusta" : null);
        }

        private List<string> daySource;

        public List<string> DaySource
        {
            get
            {
                return daySource;
            }
            set
            {
                if (daySource == value)
                {
                    return;
                }
                daySource = value;
                RaisePropertyChanged("DaySource");
                RaiseErrorsChanged();
            }
        }

        private void loadDays()
        {
            List<string> days = new List<string>();
            days.Add("Poniedziałek");
            days.Add("Wtorek");
            days.Add("Środa");
            days.Add("Czwartek");
            days.Add("Piątek");
            DaySource = days;
        }


        object[] duty = new object[3]; 
        private void OnSave(object _)
        {
            Duty d = new Duty(); // nie powinno być tak, powinno być "zewnątrzpo interfejsie"
            d.UpdateDuty(duty_id, SelectedDay, HourFrom, HourTo);
            //MainWindowViewModel.ed.Close();
            
        }

        private string _selectedDay;
        public string SelectedDay
        {
            get
            {
                return _selectedDay;
            }
            set
            {
                if (_selectedDay == value)
                {
                    return;
                }
                _selectedDay = value;
                RaisePropertyChanged("SelectedDay");
            }
        }

        private string _hourFrom;
        public string HourFrom
        {
            get
            {
                return _hourFrom;
            }
            set
            {
                if (_hourFrom == value)
                {
                    return;
                }
                _hourFrom= value;
                RaisePropertyChanged("HourFrom");
                RaiseErrorsChanged();
            }
        }

        private string _hourTo;
        public string HourTo
        {
            get
            {
                return _hourTo;
            }
            set
            {
                if (_hourTo == value)
                {
                    return;
                }
                _hourTo = value;
                RaisePropertyChanged("HourTo");
            }
        }
    }
}
