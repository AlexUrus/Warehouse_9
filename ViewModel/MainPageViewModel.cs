using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseJournal.Model;
using WarehouseJournal.Services;

namespace WarehouseJournal.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private List<Item> itemList = new();

        [ObservableProperty]
        private ObservableCollection<Item> items = new();

        [ObservableProperty]
        private Item selectedItem;

        private bool isUsingFilter;
        public bool IsUsingFilter
        {
            get 
            { 
                return isUsingFilter; 
            } 
            set 
            {
                isUsingFilter = value;
                if(value == false)
                {
                    Items = new ObservableCollection<Item>(itemList);
                    IsEnabledForFilters = false;
                }
                else
                {
                    IsEnabledForFilters = true;
                }
            }
        }

        private bool isEnabledForFilters;
        public bool IsEnabledForFilters
        {
            get
            {
                return isEnabledForFilters;
            }
            set
            {
                isEnabledForFilters = value;
                OnPropertyChanged();
            }
        }

        private string searchItemString;
        public string SearchItemString
        {
            get
            {
                return searchItemString;
            }
            set
            {
                searchItemString = value;
                SearchByName(searchItemString);
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> itemTypes;
        public ObservableCollection<string> ItemTypes
        {
            get => itemTypes;
            set
            {
                itemTypes = value;
                OnPropertyChanged();
            }
        }

        private string selectedItemType;
        public string SelectedItemType
        {
            get
            {
                return selectedItemType;
            }
            set
            {
                selectedItemType = value;
                SearchByItemType(ItemType.Types.FirstOrDefault(x => x.Type == selectedItemType));
                OnPropertyChanged();
            }
        }

        // товар в одном экзмемпляре
        private bool isSingleItem;
        public bool IsSingleItem
        {
            get
            {
                return isSingleItem;
            }
            set
            {
                isSingleItem = value;
                FilterBySingleItem(isSingleItem);
                OnPropertyChanged();
            }
        }

        public MainPageViewModel()
        {
            LoadItemsAsync();
            DataUpdater.DataUpdated += LoadItemsAsync;
        }

        async void LoadItemsAsync()
        {
            List<Item> items = await App.DataBase.GetItemsAsync();
            itemList.Clear();
            itemList.AddRange(items);
            ObservableCollection<Item> itemCollection = new ObservableCollection<Item>(itemList);
            Items = itemCollection;
            ItemTypes = new ObservableCollection<string>();

            foreach (var itemType in ItemType.Types)
            {
                ItemTypes.Add(itemType.Type);
            }
        }

        [RelayCommand]
        private async Task ChangeItem(Item item)
        {
            var navigationParameter = new Dictionary<string, object>()
            {
                {"Item", item}
            };
            await Shell.Current.GoToAsync("//MainPage/ChangeItemPage", navigationParameter);
        }

        [RelayCommand]
        private async Task DeleteItem(Item item)
        {
            await App.DataBase.DeleteItemAsync(item);
            await Application.Current.MainPage.DisplayAlert("ОК", "Товар успешно удален", "ОК");
            LoadItemsAsync();
        }

        private ObservableCollection<Item> SearchByName(string search)
        {
            if(SearchItemString != "")
            {
                Items = new ObservableCollection<Item>(Items.Where(x => x.Name.ToLower().Contains(search.ToLower())).ToList());
            }
            else
            {
                Items = new ObservableCollection<Item>(itemList);
            }
            return Items;
        }

        private ObservableCollection<Item> SearchByItemType(ItemType itemType)
        {
            if (itemType != ItemType.Types[0])
            {
                Items = new ObservableCollection<Item>(Items.Where(x => x.ItemType == itemType.Type).ToList());
            }
            else
            {
                Items = new ObservableCollection<Item>(itemList);
            }
            return Items;
        }

        private ObservableCollection<Item> FilterBySingleItem(bool isSingleItem)
        {
            if (isSingleItem)
            {
                Items = new ObservableCollection<Item>(Items.Where(x => x.Count == 1).ToList());
            }
            else
            {
                Items = new ObservableCollection<Item>(itemList);
            }
            return Items;
        }
    }
}
