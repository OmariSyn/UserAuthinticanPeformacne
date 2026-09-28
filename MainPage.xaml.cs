using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace PerformanceApp
{
    public partial class MainPage : ContentPage
    {
        private readonly HttpClient _httpClient;

        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:35662/api/") // Update this to the PerformanceAuth API base URL
            };
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            try
            {
                // Retrieve User ID and Password from Entry fields
                string userId = UserIdEntry.Text;
                string password = PasswordEntry.Text;

                // Validate credentials
                if (userId == "Johnson01" && password == "Password1")
                {
                    // Add Basic Authentication header
                    var authHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{userId}:{password}"));
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeader);

                    // Make a test request to verify authentication
                    var response = await _httpClient.GetAsync("performanceauth/values");
                    if (response.IsSuccessStatusCode)
                    {
                        // Navigate to the data entry page
                        await Navigation.PushAsync(new DataEntryPage());
                    }
                    else
                    {
                        await DisplayAlert("Error", "Failed to authenticate with the server.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Login Failed", "Invalid User ID or Password.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            // Clear User ID, Password, and any displayed messages
            UserIdEntry.Text = string.Empty;
            PasswordEntry.Text = string.Empty;
        }
    }

    // Add this class if it doesn't exist in your project
    public class DataEntryPage : ContentPage
    {
        public DataEntryPage()
        {
            Content = new Label
            {
                Text = "Welcome to the Data Entry Page!",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
        }
    }
}