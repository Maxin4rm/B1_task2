namespace B1_task2.Server.Models;

public class File
{
    public int FileID { get; set; }
    public int BalanceID { get; set; }
    public required string FileName { get; set; }
    public required string FileInfo { get; set; }
    public Balance Balance { get; set; } = null!;
}
