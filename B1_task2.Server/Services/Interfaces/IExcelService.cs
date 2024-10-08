using B1_task2.Server.DTO.ResponseDTO;
using File = B1_task2.Server.Models.File;

namespace B1_task2.Server.Services.Interfaces
{
    public interface IExcelService
    {
        public Task<List<File>> GetFilesAsync();
        public Task<FileDataResponseDTO?> GetFileDataAsync(int fileId);
        public Task<bool> UploadFileAsync(IFormFile inputfile);
    }
}
