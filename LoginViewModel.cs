namespace bibaboba3
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        // Модель для данных
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged("Username");
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        // Команда для выполнения операции аутентификации
        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(() => Login(), () => CanLogin());
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private readonly Database _database = new Database();

        private void Login()
        {
            if (_database.Auth(Username, Password))
            {
                // Успешная авторизация
                // Можно добавить дальнейшие действия
                MessageBox.Show("Login successful!");
            }
            else
            {
                // Ошибка входа
                MessageBox.Show("Incorrect username or password");
            }
        }
    }
}
