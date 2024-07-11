namespace Api.Domain.DataTransfer.Session
{
    public class AuthSession
    {
        public String Key { get; set; }
        public String Issuer { get; set; }

        public AuthSession() { }

        public AuthSession(string key, string issuer)
        {
            Key = key;
            Issuer = issuer;
        }
    }
}
