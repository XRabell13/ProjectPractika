using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ProjectPractika
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(name));
        }
    }

}
