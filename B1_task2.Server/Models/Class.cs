namespace B1_task2.Server.Models;

public class Class
{
    public int ClassID { get; set; }
    public int ClassBalanceID { get; set; }
    public required string ClassName { get; set; }
    public Balance ClassBalance { get; set; } = null!;
}
