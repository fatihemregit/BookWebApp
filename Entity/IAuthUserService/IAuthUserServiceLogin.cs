namespace Entity.IAuthUserService
{
    public class IAuthUserServiceLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Persistent { get; set; }
    }
}