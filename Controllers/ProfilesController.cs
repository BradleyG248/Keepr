using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Keepr.Controllers
{
  [ApiController]
  [Route("api/profile")]
  public class ProfilesController : ControllerBase
  {
    private readonly ProfilesService _ps;
    private readonly KeepsService _ks;
    public ProfilesController(ProfilesService ps, KeepsService ks)
    {
      _ps = ps;
      _ks = ks;
    }
    [HttpGet]
    [Authorize]
    public ActionResult<Profile> Get()
    {
      try
      {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ps.Get(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      };
    }
    [HttpPost]
    [Authorize]
    public ActionResult<Profile> Post([FromBody] Profile newProfile)
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        newProfile.UserId = userId;
        return Ok(_ps.Create(newProfile));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    [HttpPut]
    [Authorize]
    public ActionResult<Profile> Put([FromBody] Profile editedProfile)
    {
      var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
      editedProfile.UserId = userId;
      return Ok(_ps.Edit(editedProfile));
    }
    [HttpDelete]
    [Authorize]
    public ActionResult<Profile> Delete()
    {
      try
      {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        return Ok(_ps.Delete(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  }
}