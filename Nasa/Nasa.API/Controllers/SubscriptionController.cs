﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Nasa.BLL.ServicesContracts;
using Nasa.Common.DTO.Subscribe;

namespace Nasa.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubscriptionController: ControllerBase
{
    private readonly ISubscribeService _subscribeService;

    public SubscriptionController(ISubscribeService subscribeService)
    {
        _subscribeService = subscribeService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<SubscribeDto>>> GetAllSubscriptions()
    {
        return Ok(await _subscribeService.GetAllUserSubscription());
    }

    [HttpPost]
    public async Task<ActionResult> Subscribe(SubscribeDto subscribeDto)
    {
        await _subscribeService.SubscribeAsync(subscribeDto);
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<ActionResult> Unsubscribe(SubscribeDto subscribeDto)
    {
        await _subscribeService.UnsubscribeAsync(subscribeDto);
        return NoContent();
    }
}