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
    [QueryProperty(nameof(Name), nameof(Name))]
    [QueryProperty(nameof(Count), nameof(Count))]
    [QueryProperty(nameof(Cost), nameof(Cost))]
    [QueryProperty(nameof(SelectedItemType), nameof(SelectedItemType))]
    public partial class AddItemPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private int count;

        [ObservableProperty]
        private double cost;

        [ObservableProperty]
        private string selectedItemType = string.Empty;

        [ObservableProperty]
        private ObservableCollection<string> itemTypes;

        public AddItemPageViewModel()
        {
            ItemTypes = new ObservableCollection<string>();
            foreach (var itemType in ItemType.Types)
            {
                ItemTypes.Add(itemType.Type);       
            }
        }

        [RelayCommand]
        private async Task AddItem()
        {
            if(Name != string.Empty && Count > 0 && Cost > 0 && SelectedItemType != string.Empty)
            {
                await App.DataBase.SaveItemAsync(new Model.Item
                {
                    Name = this.Name,
                    Count = this.Count,
                    Cost = this.Cost,
                    ItemType = this.SelectedItemType
                });

                await Application.Current.MainPage.DisplayAlert("ОК", "Товар добавлен", "ОК");
                ClearFields();
                DataUpdater.InformAboutDataUpdateEvent();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Заполните все поля инфорации о товаре", "ОК");
            }
        }

        [RelayCommand]
        private async Task QRScan()
        {
            await Shell.Current.GoToAsync("//AddItemPage/QRScannerPage");
        }


        private void ClearFields()
        {
            Name = string.Empty;
            Count = 0;
        }
    }
}
