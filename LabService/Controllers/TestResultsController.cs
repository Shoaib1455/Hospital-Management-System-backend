using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;

namespace LabService.Controllers;

public class UploadTestResultDto
{
    public string Result { get; set; } = string.Empty;
    public string? ReportUrl { get; set; }
}

[ApiController]
[Route("api/lab/tests")]
public class TestResultsController(IHttpClientFactory clients) : ControllerBase
{
    [HttpPut("{testId:guid}/result")]
    public async Task<IActionResult> UploadResult(Guid testId, UploadTestResultDto dto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(dto.Result)) return BadRequest("A test result is required.");
        using var response = await clients.CreateClient("patient-service")
            .PutAsJsonAsync($"api/lab/tests/{testId}/result", dto, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        return new ContentResult { StatusCode = (int)response.StatusCode, Content = content, ContentType = "application/json" };
    }
}
