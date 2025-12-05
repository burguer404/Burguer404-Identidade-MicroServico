namespace Identidade.Domain.Arguments.AuthAzureAd
{
    public class AuthResponse
    {
        public bool success { get; set; }
        public string message { get; set; } = string.Empty;
    }
}
