using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Vault> Get(string UserId)
    {
      string sql = "SELECT * FROM vaults WHERE userId = @UserId";
      IEnumerable<Vault> vault = _db.Query<Vault>(sql, new { UserId });
      return vault;
    }
    internal Vault Get(int Id)
    {
      string sql = "SELECT * FROM vaults WHERE id = @Id";
      return _db.QueryFirstOrDefault<Vault>(sql, new { Id });
    }

    internal Vault Create(Vault VaultData)
    {
      string sql = @"
            INSERT INTO vaults
            (name, description, userId)
            VALUES
            (@Name, @Description, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      VaultData.Id = _db.ExecuteScalar<int>(sql, VaultData);
      return VaultData;
    }
    internal void Edit(Vault EditedVault)
    {
      string sql = @"
        UPDATE vaults
        SET
            name = @Name,
            description = @Description
        WHERE id = @Id;
        ";
      _db.Execute(sql, EditedVault);
    }
    internal bool Delete(int Id)
    {
      string sql = "DELETE FROM vaults WHERE id = @Id LIMIT 1";
      int deleted = _db.Execute(sql, new { Id });
      return deleted == 1;
    }
  }
}