using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _repo;
    public ProfilesService(ProfilesRepository repo)
    {
      _repo = repo;
    }
    public Profile Get(string UserId)
    {
      Profile profile = _repo.Get(UserId);
      if (profile == null)
      {
        Profile newProfile = new Profile();
        newProfile.UserId = UserId;
        newProfile.Name = "User";
        newProfile.Email = "Email";
        return Create(newProfile);
      }
      return profile;
    }

    public Profile Create(Profile newProfile)
    {
      return _repo.Create(newProfile);
    }
    public Profile Edit(Profile EditedProfile)
    {
      Profile original = Get(EditedProfile.UserId);
      original.Name = EditedProfile.Name != null ? EditedProfile.Name : original.Name;
      original.Email = EditedProfile.Email != null ? EditedProfile.Email : original.Email;
      original.Img = EditedProfile.Img != null ? EditedProfile.Img : original.Img;
      _repo.Edit(original);
      return original;
    }
    public string Delete(string UserId)
    {
      Profile profile = Get(UserId);
      if (_repo.Delete(UserId))
      {
        return "Deleted.";
      }
      throw new Exception("Delete Failed.");
    }
  }
}