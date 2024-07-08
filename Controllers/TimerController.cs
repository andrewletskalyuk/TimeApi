using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimeApi.Models;
using TimeApi.Services;

namespace TimeApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimerController : ControllerBase
{
	readonly ITimerService _timerService;

	public TimerController(ITimerService timerService)
	{
		_timerService = timerService;
	}

	[HttpPost("set")]
	public ActionResult<TimerResponce> SetTimer([FromBody] SetTimerRequest request)
	{
		var timerResponse = _timerService.SetTimer(request);
		return Ok(timerResponse);
	}

	[HttpGet("status/{id}")]
	public ActionResult<TimerStatusResponce> GetTimerStatus(string id)
	{
		var timerStatusResponce = _timerService.GetTimerStatus(id);
		return Ok(timerStatusResponce);
	}

	[HttpGet("list")]
	public ActionResult<TimersListResponce> ListTimers([FromQuery] int pageNumber, [FromQuery] int pageSize = 100)
	{
		var timersListResponce = _timerService.ListTimers(pageNumber, pageSize);
		return Ok(timersListResponce);
	}
}
