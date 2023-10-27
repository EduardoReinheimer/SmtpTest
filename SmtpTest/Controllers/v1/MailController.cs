﻿using Api.Entities.Mail;
using Api.Interfaces;
using FluentEmail.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1;

[ApiController]
[Route("/api/[controller]/[action]")]
public class MailController : ControllerBase
{
    private readonly IEmailService _service;
    private readonly ILogger<MailController> _logger;

    public MailController(IEmailService service, ILogger<MailController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<string>> SendTest()
    {

        var response = await _service.SendTestMail();
        return response
            ? Ok(response)
            : Problem("Deu ruim");
    }

    [ProducesResponseType(typeof(SendResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(SendResponse), StatusCodes.Status503ServiceUnavailable)]
    [HttpPost]
    public async Task<ActionResult<SendResponse>> SendMail(RequestSendMail body)
    {
        var result = await _service.SendMailAsync(body);

        return result.Successful
            ? Ok(result)
            : StatusCode(
                StatusCodes.Status503ServiceUnavailable,
                result);
    }


}