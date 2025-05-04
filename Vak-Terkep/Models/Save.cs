public class Save
{
    public int Id { get; set; }
    public string buildingName { get; set; }
    public string floorName { get; set; }

    public string textName { get; set; }
    public string Description { get; set; }
    public int AccountId { get; set; } 
 

    public Account Accounts { get; set; }
}