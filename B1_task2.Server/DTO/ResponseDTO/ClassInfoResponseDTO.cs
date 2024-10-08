using B1_task2.Server.Models;

namespace B1_task2.Server.DTO.ResponseDTO
{
    public class ClassInfoResponseDTO
    {
        public Class Class { get; set; } = null!;
        public List<AccountBalanceResponseDTO> AccountBalances { get; set; } = null!;
        public Balance ClassBalance { get; set; } = null!;
    }
}
