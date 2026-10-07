[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
private readonly IStorageService _storageService;
private readonly IEmailService _emailService;
public ApplicationController(IStorageService storageService,
IEmailService emailService)
{
_storageService = storageService;
_emailService = emailService;
}

[HttpPost]
public async Task<IActionResult> SaveUploadedResume()
{
        Request.EnableBuffering();
        using var fileStream = new MemoryStream();
        using var reader = new StreamReader(fileStream);
        await Request.Body.CopyToAsync(fileStream);
        var storedFileUrl = await _storageService.Upload(fileStream);
        await _emailService.Send("recruitment@example.com",
        $"Somebody has uploaded a resume! Read it here: {storedFileUrl}");
        return Ok();
}
}