using WarehouseJournal.View;

namespace WarehouseJournal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"{nameof(MainPage)}/{nameof(ChangeItemPage)}", typeof(ChangeItemPage));
            Routing.RegisterRoute($"{nameof(AddItemPage)}/{nameof(QRScannerPage)}", typeof(QRScannerPage));
        }
    }
}
