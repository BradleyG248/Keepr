using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _repo;
    public VaultKeepsService(VaultKeepsRepository repo)
    {
      _repo = repo;
    }
    public IEnumerable<VaultKeep> Get(string userId)
    {
      return _repo.Get(userId);
    }
    public VaultKeep Get(int Id, string userId)
    {
      VaultKeep vault = _repo.Get(Id);
      if (vault == null)
      {
        throw new Exception("VaultKeep not found");
      }
      return vault;
    }
    public IEnumerable<Keep> GetKeepsByVaultId(int VaultId, string UserId)
    {
      return _repo.GetKeepsByVaultId(VaultId, UserId);
    }

    public VaultKeep Create(VaultKeep newVaultKeep)
    {
      return _repo.Create(newVaultKeep);
    }
    public VaultKeep Edit(VaultKeep EditedVaultKeep)
    {
      VaultKeep original = Get(EditedVaultKeep.Id, EditedVaultKeep.UserId);
      original.KeepId = EditedVaultKeep.KeepId != 0 ? EditedVaultKeep.KeepId : original.KeepId;
      original.VaultId = EditedVaultKeep.VaultId != 0 ? EditedVaultKeep.VaultId : original.VaultId;
      _repo.Edit(original);
      return original;
    }
    public string Delete(int Id, string UserId)
    {
      VaultKeep vault = Get(Id, UserId);
      if (vault.UserId != UserId)
      {
        throw new Exception("That's not your vault.");
      }
      if (_repo.Delete(Id, UserId))
      {
        return "Deleted.";
      }
      throw new Exception("Delete Failed.");
    }
  }
}