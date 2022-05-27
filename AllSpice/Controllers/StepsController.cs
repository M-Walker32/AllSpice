using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using AllSpice.Models;
using AllSpice.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class StepsController : ControllerBase
  {
    private readonly StepsService _ss;
    public StepsController(StepsService ss)
    {
      _ss = ss;
    }
    // // GET
    // [HttpGet]
    // public ActionResult<List<Step>> Get()
    // {
    //   try
    //   {
    //     List<Step> step = _ss.Get();
    //     return Ok(step);
    //   }
    //   catch (Exception e)
    //   {
    //     return Ok(e.Message);
    //   }
    // }
    // GET BY ID
    [HttpGet("{id}")]
    public ActionResult<List<Step>> Get(int id)
    {
      try
      {
        Step step = _ss.Get(id);
        return Ok(step);
      }
      catch (Exception e)
      {
        return Ok(e.Message);
      }
    }
    // CREATE
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Step>> Create([FromBody] Step stepData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        stepData.CreatorId = userInfo.Id;
        Step step = _ss.Create(stepData);
        step.Creator = userInfo;
        return Ok(step);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // Delete
    [HttpDelete("{id}")]
    [Authorize]

    public async Task<ActionResult<Step>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ss.Delete(id, userInfo.Id);
        return Ok("delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}