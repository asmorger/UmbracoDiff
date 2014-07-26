using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Caliburn.Micro;
using PropertyChanged;

namespace UmbracoDiff.ViewModels.Dialogs
{
    [ImplementPropertyChanged]
    public class DbConnectionDialogViewModel : Screen
    {
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}
