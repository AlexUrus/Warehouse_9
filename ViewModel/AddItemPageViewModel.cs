using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
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
    [QueryProperty(nameof(ItemType), nameof(ItemType))]
    public partial class AddItemPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private string name = string.Empty;

        [ObservableProperty]
        private int count;

        [ObservableProperty]
        private double cost;

        [ObservableProperty]
        private ItemType itemType = ItemType.None;

        [ObservableProperty]
        private List<string> itemTypes;

        [ObservableProperty]
        private string selectedItemType;

        [RelayCommand]
        private async Task AddItem()
        {
            if(Name != string.Empty && Count > 0 && Cost > 0)
            {
                await App.DataBase.SaveItemAsync(new Model.Item
                {
                    Name = this.Name,
                    Count = this.Count,
                    Cost = this.Cost,
                    ItemType = this.ItemType
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
