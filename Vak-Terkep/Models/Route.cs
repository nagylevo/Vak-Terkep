using System.ComponentModel.DataAnnotations.Schema;

public class Route
{
	public int Id { get; set; }
	public string floorName { get; set; }
	public string textName { get; set; }
	public string buildingName { get; set; }
	public string description { get; set; }
}