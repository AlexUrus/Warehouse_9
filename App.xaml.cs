using WarehouseJournal.Services;

namespace WarehouseJournal
{
    public partial class App : Application
    {
        private const string DbName = "Item.db";
        private static string _dbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);

        private static DataBaseService dataBase;
        public static DataBaseService DataBase
        {
            get
            {
                if(dataBase == null)
                {
                    dataBase = new DataBaseService(_dbPath);
                }
                return dataBase;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
