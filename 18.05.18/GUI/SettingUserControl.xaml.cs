using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for SettingUserControl.xaml
    /// </summary>
    public partial class SettingUserControl : UserControl
    {
        private SettingsViewModal svm;
        public SettingUserControl()
        {
            InitializeComponent();
            svm = new SettingsViewModal(new SettingModal());
            this.DataContext = svm;
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            svm.ChangedPropertyTest();
        }
    }
}
