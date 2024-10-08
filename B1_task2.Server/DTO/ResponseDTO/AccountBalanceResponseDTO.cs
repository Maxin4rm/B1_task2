namespace B1_task2.Server.DTO.ResponseDTO
{
    public class AccountBalanceResponseDTO
    {
        public int AccountID { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public decimal IncomingActive { get; set; }
        public decimal IncomingPassive { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal OutgoingActive { get; set; }
        public decimal OutgoingPassive { get; set; }
    }
}
