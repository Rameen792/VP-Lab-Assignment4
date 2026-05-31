namespace App05_AuthenticationState.Services
{
    public class AuthenticationStateService
    {
        public bool IsAuthenticated { get; private set; }

        public event Action? OnChange;

        public void LogIn()
        {
            IsAuthenticated = true;
            NotifyStateChanged();
        }

        public void LogOut()
        {
            IsAuthenticated = false;
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}