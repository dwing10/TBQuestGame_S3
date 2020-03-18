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
using System.Windows.Shapes;
using TBQuestGame_S1.PresentationLayer;

namespace TBQuestGame_S1.PresentationLayer
{
    /// <summary>
    /// Interaction logic for InventoryView.xaml
    /// </summary>
    public partial class InventoryView : Window
    {
        GameSessionViewModel _gameSessionViewModel;
        public InventoryView(GameSessionViewModel gameSessionViewModel)
        {
            _gameSessionViewModel = gameSessionViewModel;
            InitializeComponent();
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UseItem_Button_Click_1(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.OnUseGameItem();
        }

        private void DropItem_Button_Click(object sender, RoutedEventArgs e)
        {
            _gameSessionViewModel.RemoveItemFromInventory();
        }
    }
}
