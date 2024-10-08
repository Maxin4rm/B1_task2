namespace B1_task2.Server.Models;

public class Account
{
    public int AccountID { get; set; }
    public required string AccountNumber { get; set; }
    public int ClassID { get; set; }
    public int FileID { get; set; }
    public required Class Class { get; set; }
    public required File File { get; set; }
}
