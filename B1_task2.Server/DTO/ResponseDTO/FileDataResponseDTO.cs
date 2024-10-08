using B1_task2.Server.Controllers;
using B1_task2.Server.Models;

namespace B1_task2.Server.DTO.ResponseDTO
{
    public class FileDataResponseDTO
    {
        public FileInfoResponseDTO File { get; set; } = null!;
        public List<ClassInfoResponseDTO> Classes { get; set; } = null!;
        public Balance FileBalance { get; set; } = null!;  
    }
}
