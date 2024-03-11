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
    [QueryProperty (nameof(Item), nameof(Item))]
    public partial class ChangeItemPageViewModel: ObservableObject
    {
        [ObservableProperty]
        private Item item;

        [RelayCommand]
        private async Task ChangeItem()
        {
            if(Item.Name != string.Empty && Item.Count > 0)
            {
                await App.DataBase.UpdateItemAsync(Item);
                await Application.Current.MainPage.DisplayAlert("ОК", "Товар успешно изменен", "ОК");
                DataUpdater.InformAboutDataUpdateEvent();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", "Заполните все поля инфорации о товаре", "ОК");
            }
        }
    }
}
