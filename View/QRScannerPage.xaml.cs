namespace WarehouseJournal.View;

using System.Text.Json;
using WarehouseJournal.Model;

public partial class QRScannerPage : ContentPage
{
	private Item item = null;

	public QRScannerPage()
	{
		InitializeComponent();
	}

	private void BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
	{
		Dispatcher.DispatchAsync(async () => 
		{
			string str = $"{e.Results[0].Value}";
			item = JsonSerializer.Deserialize<Item>(str);
			var navigationParameter = new Dictionary<string, object>()
			{
				{"Name", item.Name },
				{"Count", item.Count }	
			};

			barcodereader.IsDetecting = false;
            await Shell.Current.GoToAsync("//AddItemPage", navigationParameter);
        });
	}
}