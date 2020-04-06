using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _repo;
    public VaultsService(VaultsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<Vault> Get(string userId)
    {
      return _repo.Get(userId);
    }
    public Vault Get(int Id, string userId)
    {
      Vault vault = _repo.Get(Id);
      if (vault == null)
      {
        throw new Exception("Vault not found");
      }
      return vault;
    }

    public Vault Create(Vault newVault)
    {
      return _repo.Create(newVault);
    }
    public Vault Edit(Vault EditedVault)
    {
      Vault original = Get(EditedVault.Id, EditedVault.UserId);
      original.Name = EditedVault.Name != null ? EditedVault.Name : original.Name;
      original.Description = EditedVault.Description != null ? EditedVault.Description : original.Description;
      _repo.Edit(original);
      return original;
    }
    public string Delete(int Id, string UserId)
    {
      Vault vault = Get(Id, UserId);
      if (vault.UserId != UserId)
      {
        throw new Exception("That's not your vault.");
      }
      if (_repo.Delete(Id))
      {
        return "Deleted.";
      }
      throw new Exception("Delete Failed.");
    }
  }
}