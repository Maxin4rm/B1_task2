using B1_task2.Server.Data;
using B1_task2.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace B1_task2.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExcelController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IExcelService _excelService;

    public ExcelController(AppDbContext context, IExcelService excelService)
    {
        _context = context;
        _excelService = excelService;
    }

    [HttpGet("files")]
    public async Task<IActionResult> GetFiles()
    {
        var files = await _excelService.GetFilesAsync();
        return Ok(files);
    }

    [HttpGet("filedata/{fileId}")]
    public async Task<IActionResult> GetFileData(int fileId)
    {
        var response = await _excelService.GetFileDataAsync(fileId);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile inputfile)
    {
        var result = await _excelService.UploadFileAsync(inputfile);
        if (result)
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }  
    }
}