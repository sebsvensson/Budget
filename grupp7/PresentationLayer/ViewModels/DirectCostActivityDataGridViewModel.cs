using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModels
{
    public class DirectCostActivityDataGridViewModel : BaseViewModel
    {
        private DataTable _table;
        public DataTable Table
        {
            get { return _table; }
            set
            {
                _table = value;
                OnPropertyChanged(null);
            }
        }

        public DirectCostActivityDataGridViewModel(DataTable table)
        {
            Table = table;
        }
    }
}
