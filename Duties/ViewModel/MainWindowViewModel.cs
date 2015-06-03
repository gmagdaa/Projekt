using Duties.Base;
using Duties.Model;
using Duties.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Duties.ViewModel
{
    class MainWindowViewModel : NotificationObject
    {
        const int MODE_VIEW = 0;
        const int MODE_EDIT = 1;
        const int MODE_ADD = 2;

        private DataView _unitsSource;

        public DataView UnitsSource
        {
            get
            {
                return _unitsSource;
            }
            set
            {
                if (_unitsSource == value)
                {
                    return;
                }
                _unitsSource = value;
                RaisePropertyChanged("UnitsSource");
            }
        }

        private string _selectedUnit;

        public string SelectedUnit
        {
            get
            {
                return _selectedUnit;
            }
            set
            {
                if (_selectedUnit == value)
                {
                    return;
                }
                _selectedUnit = value;
                loadStaff(Int32.Parse(value));
                SelectedStaff = null;
                RaisePropertyChanged("selectedUnit");
            }
        }

        private DataView _staffSource;

        public DataView StaffSource
        {
            get
            {
                return _staffSource;
            }
            set
            {
                if (_staffSource == value)
                {
                    return;
                }
                _staffSource = value;
                RaisePropertyChanged("StaffSource");
            }
        }

        private string _selectedStaff;
        public string SelectedStaff
        {
            get
            {
                return _selectedStaff;
            }
            set
            {
                if (_selectedStaff == value)
                {
                    return;
                }
                _selectedStaff = value;
                if (value != null)
                    loadDuties(Int32.Parse(value));
                else
                    DutySource = null;
                RaisePropertyChanged("SelectedStaff");
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private DataView _dutySource;
        public DataView DutySource
        {
            get
            {
                return _dutySource;
            }
            set
            {
                if (_dutySource == value)
                {
                    return;
                }
                _dutySource = value;
                RaisePropertyChanged("DutySource");
            }
        }

        private string _selectedDuty;
        public string SelectedDuty
        {
            get
            {
                return _selectedDuty;
            }
            set
            {
                if (_selectedDuty == value)
                {
                    return;
                }
                _selectedDuty = value;
                RaisePropertyChanged("SelectedDuty");
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        private int _currentMode;

        public int CurrentMode
        {
            get
            {
                return _currentMode;
            }
            set
            {
                if (_currentMode == value)
                {
                    return;
                }
                _currentMode = value;
                RaisePropertyChanged("CurrentMode");
            }
        }

        private AddDutyViewModel _addDuty;

        public AddDutyViewModel AddDuty
        {
            get
            {
                return _addDuty;
            }
            set
            {
                _addDuty = value;
                RaisePropertyChanged();
            }
        }

        private EditDutyViewModel _editDuty;

        public EditDutyViewModel EditDuty
        {
            get
            {
                return _editDuty;
            }
            set
            {
                _editDuty = value; RaisePropertyChanged();
            }
        }

        public MainWindowViewModel()
        {
            loadUnits();
            CurrentMode = MODE_VIEW;
        }

        private void loadUnits()
        {
            Units u = new Units();
            UnitsSource = u.Select().DefaultView;
        }

        private void loadStaff(int unit_id)
        {
            Staff s = new Staff();
            StaffSource = s.SelectByUnit(unit_id).DefaultView;
        }

        private void loadDuties(int staff_id)
        {
            Duty d = new Duty();
            DutySource = d.SelectByStaff(staff_id).DefaultView;
        }

        /* private void editDuties()
        {
            Duty d = new Duty();
            d.UpdateByStaff(Int32.Parse(SelectedStaff), DutySource.ToTable());
        } */

        // editCommand
        private BasicCommand _editCommand;

        public BasicCommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new BasicCommand(OnEdit, CanEdit)); }
        }

        private void EditDutyErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        private void OnEdit(object _)
        {
            EditDutyViewModel editDuty = new EditDutyViewModel(Int32.Parse(SelectedDuty));
            editDuty.ErrorsChanged += EditDutyErrorsChanged;
            EditDuty = editDuty;
            CurrentMode = MODE_EDIT;
        }

        private bool CanEdit(object _)
        {
            return (SelectedDuty != null);
        }

        //addCommand
        private BasicCommand _addCommand;

        public BasicCommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new BasicCommand(OnAdd, CanAdd)); }
        }

        private void AddDutyErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();                       
        }

        private void OnAdd(object _)
        {
            AddDutyViewModel addDuty = new AddDutyViewModel(Int32.Parse(SelectedStaff));
            addDuty.ErrorsChanged += AddDutyErrorsChanged;
            AddDuty = addDuty;
            CurrentMode = MODE_ADD;
        }

        private bool CanAdd(object _)
        {
            return (SelectedStaff != null);
        }

        //deleteCommand
        private BasicCommand _deleteCommand;

        public BasicCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new BasicCommand(OnDelete, CanDelete)); }
        }

        private void OnDelete(object _)
        {
            bool message;
            Duty d = new Duty();
            message = ConfirmationBox.ShowMessage("Czy na pewno chcesz usunąć ten dyżur?", "Usuwanie dyżuru");
            if (message == true)
            {
            d.DeleteDuty(Int32.Parse(SelectedDuty));
            loadDuties(Int32.Parse(SelectedStaff));
            }
        }

        private bool CanDelete(object _)
        {
            return (SelectedDuty != null);
        }

        private BasicCommand _printCommand;

        public BasicCommand PrintCommand
        {
            get { return _printCommand ?? (_printCommand = new BasicCommand(OnPrint, CanPrint)); }
        }

        private void OnSave(object _)
        {
            Duty d = new Duty();
            switch (CurrentMode)
            {
                case MODE_ADD:
                    d.AddDuty(Int32.Parse(SelectedStaff), AddDuty.SelectedDay, AddDuty.HourFrom, AddDuty.HourTo);
                    break;
                case MODE_EDIT:
                    d.UpdateDuty(Int32.Parse(SelectedDuty), EditDuty.SelectedDay, EditDuty.HourFrom, EditDuty.HourTo);
                    break;
            }
            loadDuties(Int32.Parse(SelectedStaff));
            CurrentMode = MODE_VIEW;
        }

        private bool CanSave(object _)
        {
            switch (CurrentMode)
            {
                case MODE_ADD:
                    return !AddDuty.HasErrors;
                case MODE_EDIT:
                    return !EditDuty.HasErrors;
            }
            return false;
        }

        private BasicCommand _saveCommand;

        public BasicCommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new BasicCommand(OnSave, CanSave)); }
        }

        private void OnCancel(object _)
        {
            if (CurrentMode == MODE_ADD)
            {
                AddDuty = null;
            }
            if (CurrentMode == MODE_EDIT)
            {
                EditDuty = null;
            }
            CurrentMode = MODE_VIEW;
        }

        private bool CanCancel(object _)
        {
            return true;
        }

        private BasicCommand _cancelCommand;

        public BasicCommand CancelCommand
        {
            get { return _cancelCommand ?? (_cancelCommand = new BasicCommand(OnCancel, CanCancel)); }
        }

        private void OnPrint(object _)
        {
            Print p = new Print();
            p.print();
        }

        private bool CanPrint(object _)
        {
            return true;
        }

    }
}

