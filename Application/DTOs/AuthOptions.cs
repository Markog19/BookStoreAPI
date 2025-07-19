namespace BookStoreAPI.Application.DTOs
{
    public class AuthOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; } = "BookStoreAPI";

        public string Audience { get; set; } = "BookStoreClients";

    }
}
