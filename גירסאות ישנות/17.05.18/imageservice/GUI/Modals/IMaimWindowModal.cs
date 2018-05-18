using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI
{
	interface IMaimWindowModal: INotifyPropertyChanged
	{
		bool Connected { get; set; }
	}
}
