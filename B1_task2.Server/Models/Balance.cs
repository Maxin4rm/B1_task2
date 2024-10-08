namespace B1_task2.Server.Models;

public class Balance
{
    public int BalanceID { get; set; }
    public int? AccountID { get; set; }
    public decimal IncomingActive { get; set; }
    public decimal IncomingPassive { get; set; }
    public decimal Debit { get; set; }
    public decimal Credit { get; set; }
    public decimal OutgoingActive { get; set; }
    public decimal OutgoingPassive { get; set; }
    public Account? Account { get; set; }
}
