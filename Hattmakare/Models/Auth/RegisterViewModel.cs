using Hattmakare.Models.User;

namespace Hattmakare.Models.Auth;

public class RegisterViewModel
{
    public AddUserViewModel User { get; set; }
    public bool Locked { get; set; } = false;
}
