using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<VaultKeep> Get(string UserId)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE userId = @UserId";
      IEnumerable<VaultKeep> vaultkeep = _db.Query<VaultKeep>(sql, new { UserId });
      return vaultkeep;
    }
    // internal IEnumerable<Vault> GetVaultsByKeepId(int KeepId)
    // {
    //   string sql = "SELECT * FROM vaultkeeps WHERE keepId = @KeepId";
    //   IEnumerable<Vault> vaults = _db.Query<Vault>(sql, new { KeepId });
    //   return vaults;
    // }
    internal IEnumerable<Keep> GetKeepsByVaultId(int VaultId, string UserId)
    {
      string sql = @"SELECT 
        k.*,
        vk.id as vaultKeepId
        FROM vaultkeeps vk
        INNER JOIN keeps k ON k.id = vk.keepId 
        WHERE (vaultId = @VaultId AND vk.userId = @UserId)";
      IEnumerable<Keep> vaults = _db.Query<Keep>(sql, new { VaultId, UserId });
      return vaults;
    }
    internal VaultKeep Get(int Id)
    {
      string sql = "SELECT * FROM vaultkeeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { Id });
    }

    internal VaultKeep Create(VaultKeep VaultKeepData)
    {
      string sql = @"
            INSERT INTO vaultkeeps
            (keepId, vaultId, userId)
            VALUES
            (@KeepId, @VaultId, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      VaultKeepData.Id = _db.ExecuteScalar<int>(sql, VaultKeepData);
      return VaultKeepData;
    }
    internal void Edit(VaultKeep EditedVaultKeep)
    {
      string sql = @"
        UPDATE vaultkeeps
        SET
            vaultId = @VaultId,
            keepId = @KeepId
        WHERE id = @Id;
        ";
      _db.Execute(sql, EditedVaultKeep);
    }
    internal bool Delete(int Id, string UserId)
    {
      string sql = "DELETE FROM vaultkeeps WHERE (id = @Id AND userId = @UserId) LIMIT 1";
      int deleted = _db.Execute(sql, new { Id, UserId });
      return deleted == 1;
    }
  }
}