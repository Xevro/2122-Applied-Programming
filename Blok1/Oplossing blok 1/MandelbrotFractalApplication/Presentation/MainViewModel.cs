using CommunityToolkit.Mvvm.ComponentModel;
using MandelbrotFractalApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MandelbrotFractalApplication.Presentation
{
    public class MainViewModel : ObservableObject
    {

        public MainViewModel(ILogic logic)
        {
            //this.logic = logic;
            //SetColorCommand = new RelayCommand<string>(SetColor);
            //DoWorkCommand = new RelayCommand(async () => await DoWorkAsync(), () => !working);
            //CreateBitmap(maxColumn, maxRow);
        }
    }
}
