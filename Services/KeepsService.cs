using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _repo;
    public KeepsService(KeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Keep> Get()
    {
      return _repo.Get();
    }
    public Keep Get(int Id)
    {
      Keep keep = _repo.Get(Id);
      if (keep == null)
      {
        throw new Exception("Keep not found");
      }
      return keep;
    }
    public void Keep(int KeepId)
    {
      _repo.AddKeep(KeepId);
    }
    public Keep AuthGet(int Id, string UserId)
    {
      Keep keep = _repo.AuthGet(Id, UserId);
      if (keep == null)
      {
        throw new Exception("Keep not found");
      }
      return keep;
    }
    public IEnumerable<Keep> GetKeepsByUser(string UserId)
    {
      IEnumerable<Keep> keeps = _repo.GetKeepsByUser(UserId);
      if (keeps == null)
      {
        throw new Exception("No keeps found");
      }
      return keeps;
    }

    public Keep Create(Keep newKeep)
    {
      return _repo.Create(newKeep);
    }
    public Keep Edit(Keep EditedKeep)
    {
      Keep original = Get(EditedKeep.Id);
      if (EditedKeep.UserId != original.UserId)
      {
        throw new Exception("That's not your keep");
      }
      original.Name = EditedKeep.Name != null ? EditedKeep.Name : original.Name;
      original.Description = EditedKeep.Description != null ? EditedKeep.Description : original.Description;
      original.Img = EditedKeep.Img != null ? EditedKeep.Img : original.Img;
      _repo.Edit(original);
      return original;
    }
    public string Delete(int Id, string UserId)
    {
      if (_repo.Delete(Id))
      {
        return "Deleted.";
      }
      throw new Exception("Delete Failed.");
    }
  }
}