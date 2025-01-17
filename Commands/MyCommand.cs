﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Chart.Commands
{
    class MyCommand : ICommand
    {
        Action executeAction;
        public MyCommand(Action action)
        {
            executeAction = action;
        }

        public Action Action { get; set; }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            executeAction();
        }
    }
}