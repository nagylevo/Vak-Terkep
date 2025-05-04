using Vak_Terkep.Models;

public class Account
{
    public int Id { get; set; }
    public string userName { get; set; }
    public string emailAddress { get; set; }
    public string userPassword { get; set; }
    public string ProfilePicturePath { get; set; } = "/userImage.png";
    public ICollection<Save> Saved { get; set; }
  
}