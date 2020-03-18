using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestGame_S1.Models;
using TBQuestGame_S1.DataLayer;
using System.Collections.ObjectModel;

namespace TBQuestGame_S1.PresentationLayer
{
    public class GameSessionViewModel : ObservableObject
    {
        #region Fields
        private GameSessionViewModel _gameSessionViewModel;

        private Player _player;
        private List<string> _messages;
        private string _messageBoxContent;

        private Map _gameMap;
        private Location _currentLocation;
        private string _currentLocationName;
        private ObservableCollection<Location> _accessobleLocations;

        private GameItemQuantity _currentGameItem;

        #endregion

        #region Properties
        public GameSessionViewModel gameSessionViewModel
        {
            get { return _gameSessionViewModel; }
            set { _gameSessionViewModel = value; }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return string.Join("\n\n", _messages); }
        }

        public string MessageBoxContent
        {
            get { return _messageBoxContent; }
            set
            {
                _messageBoxContent = value;
                OnPropertyChange(nameof(MessageBoxContent));
            }
        }

        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChange(nameof(CurrentLocation));
            }
        }

        public string CurrentLocationName
        {
            get { return _currentLocationName; }
            set
            {
                _currentLocationName = value;
                OnPropertyChange("CurrentLocation");
            }
        }

        public ObservableCollection<Location> AccessibleLocations
        {
            get { return _accessobleLocations; }
            set
            {
                _accessobleLocations = value;
                OnPropertyChange(nameof(AccessibleLocations));
            }
        }

        public GameItemQuantity CurrentGameItem
        {
            get { return _currentGameItem; }
            set { _currentGameItem = value; }
        }
        #endregion

        #region Constructors

        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(Player player, List<string> initialMessage, Map gameMap, GameSessionViewModel gameSessionViewModel)
        {
            _gameSessionViewModel = gameSessionViewModel;
            _player = player;
            _gameMap = gameMap;
            _currentLocation = _gameMap.CurrentLocation;
            _accessobleLocations = new ObservableCollection<Location>();
            _messages = initialMessage;
            MessageBoxContent = "\tSelect a location to travel there.";

            InitializeView();
        }

        #endregion

        #region Methods

        /// <summary>
        /// initialize the view
        /// </summary>
        public void InitializeView()
        {
            _player.UpdateInventoryCategories();
        }

        /// <summary>
        /// add item from location to player's inventory
        /// </summary>
        public void AddItemToInventory()
        {
            if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
            {
                GameItemQuantity selectedGameItemQuantity = _currentGameItem as GameItemQuantity;

                _currentLocation.RemoveGameItemQuantityFromLocation(selectedGameItemQuantity);
                _player.AddGameItemToInventory(selectedGameItemQuantity);

            }
        }

        public void RemoveItemFromInventory()
        {
            if (_currentGameItem != null)
            {
                GameItemQuantity selectedGameItemQuantity = _currentGameItem as GameItemQuantity;

                _player.RemoveGameItemFromInventory(selectedGameItemQuantity);
            }
        }

        /// <summary>
        /// calls other methods based on the items used
        /// </summary>
        public void OnUseGameItem()
        {
            switch (_currentGameItem.GameItem)
            {
                case Buff buff:
                    ProcessBuffUse(buff);
                    RemoveItemFromInventory();
                    break;
                case SeigeWeapon seigeWeapon:
                    ProcessSeigeWeaponUse(seigeWeapon);
                    RemoveItemFromInventory();
                    break;
                default:
                    break;
            }
        }

        private void ProcessBuffUse(Buff buff)
        {
            if (buff.Id == "INS")
            {
                Player.Defense = Player.Defense + 100;
            }
            if (buff.Id == "BOL")
            {
                Player.LegionnaireNumbers += 25;
            }
            if (buff.Id == "TRI")
            {
                Player.Inventory.Add(new GameItemQuantity(new Treasure("GLD", "Gold", 1, "Gold is the key to building a powerful legion", Treasure.TreasureType.Coin), 1000));
            }
        }

        private void ProcessSeigeWeaponUse(SeigeWeapon seigeWeapon)
        {
            if (seigeWeapon.Id == "CAT")
            {
                Player.Attack += 200;
            }
            if (seigeWeapon.Id == "BAL")
            {
                Player.Attack += 100;
            }
        }

        /// <summary>
        /// update the accessible location
        /// </summary>
        private void UpdateAccessibleLocations()
        {
            _accessobleLocations.Clear();

            foreach (Location location in _gameMap.Locations)
            {
                if (location.IsAccessible == true)
                {
                    _accessobleLocations.Add(location);
                }
            }

            _accessobleLocations.Remove(_accessobleLocations.FirstOrDefault(l => l == _currentLocation));

            OnPropertyChange(nameof(AccessibleLocations));
        }

        /// <summary>
        /// timer that is used when traveling
        /// </summary>
        public void Timer()
        {
            System.Windows.Threading.DispatcherTimer travelTimer = new System.Windows.Threading.DispatcherTimer();
            travelTimer.Tick += dispatcherTimer_Tick;
            travelTimer.Interval = new TimeSpan(0, 0, 5);
            travelTimer.Start();
        }


        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MessageBoxContent = "\tTraveling to location.";
        }

        /// <summary>
        /// Binds buttons to locations
        /// </summary>
        public void Move(string tagName)
        {
            //Timer();

            switch (tagName)
            {
                case "Alheimurrinn":
                    foreach (Location location in AccessibleLocations)
                    {
                        if (tagName == location.Name)
                        {
                            CurrentLocation = location;
                        }
                    }
                    break;

                case "Qua Redi":
                    foreach (Location location in AccessibleLocations)
                    {
                        if (tagName == location.Name)
                        {
                            CurrentLocation = location;
                        }
                    }
                    break;

                case "Dore":
                    foreach (Location location in AccessibleLocations)
                    {
                        if (tagName == location.Name)
                        {
                            CurrentLocation = location;
                        }
                    }
                    break;

                case "North Bourg":
                    foreach (Location location in AccessibleLocations)
                    {
                        if (tagName == location.Name)
                        {
                            CurrentLocation = location;
                        }
                    }
                    break;

                case "South Bourg":
                    foreach (Location location in AccessibleLocations)
                    {
                        if (tagName == location.Name)
                        {
                            CurrentLocation = location;
                        }
                    }
                    break;

                default:
                    break;
            }

            MessageBoxContent = "\tChoose an action";

            UpdateAccessibleLocations();
        }

        /// <summary>
        /// Opens barracks window
        /// </summary>
        public void ShowBarracks()
        {
            BarracksView barracksView = new BarracksView(_player);
            barracksView.Show();
        }

        /// <summary>
        /// opens map window
        /// </summary>
        public void ShowMap()
        {
            MapView map = new MapView();
            map.Show();
        }

        /// <summary>
        /// opens information window
        /// </summary>
        public void ShowInfo()
        {
            InformationView info = new InformationView();
            info.Show();
        }

        /// <summary>
        /// opens inventory window
        /// </summary>
        public void ShowInventory()
        {             
            InventoryView inventoryView = new InventoryView(_gameSessionViewModel);
            inventoryView.ShowDialog();
        }

        #endregion
    }
}
