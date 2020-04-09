using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal IEnumerable<Keep> Get()
    {
      string sql = "SELECT * FROM keeps WHERE isPrivate = 0";
      IEnumerable<Keep> keep = _db.Query<Keep>(sql);
      return keep;
    }
    internal Keep Get(int Id)
    {
      string sql = "SELECT * FROM keeps WHERE id = @Id";
      return _db.QueryFirstOrDefault<Keep>(sql, new { Id });
    }
    internal Keep AuthGet(int Id, string UserId)
    {
      string sql = "SELECT * FROM keeps WHERE (id = @Id AND userId = @UserId)";
      Keep keep = _db.QueryFirstOrDefault<Keep>(sql, new { Id, UserId });
      return keep;
    }
    internal void AddKeep(int Id)
    {
      string sql = @"
        UPDATE keeps
        SET
            keeps = keeps+1
        WHERE id = @Id;
        ";
      _db.Execute(sql, new { Id });
    }
    internal void View(int Id)
    {
      string sql = @"
        UPDATE keeps
        SET
            views = views+1
        WHERE id = @Id;
        ";
      _db.Execute(sql, new { Id });
    }
    internal IEnumerable<Keep> GetKeepsByUser(string UserId)
    {
      string sql = "SELECT * FROM keeps WHERE userId = @UserId";
      IEnumerable<Keep> keeps = _db.Query<Keep>(sql, new { UserId });
      return keeps;
    }

    internal Keep Create(Keep KeepData)
    {
      string sql = @"
            INSERT INTO keeps
            (name, description, userId, img, isPrivate)
            VALUES
            (@Name, @Description, @UserId, @Img, @IsPrivate);
            SELECT LAST_INSERT_ID();
            ";
      KeepData.Id = _db.ExecuteScalar<int>(sql, KeepData);
      return KeepData;
    }
    internal void Edit(Keep EditedKeep)
    {
      string sql = @"
        UPDATE keeps
        SET
            name = @Name,
            description = @Description,
            img = @Img,
            views = @Views,
            keeps = @Keeps,
            shares = @Shares,
            isPrivate = @IsPrivate
        WHERE id = @Id;
        ";
      _db.Execute(sql, EditedKeep);
    }
    internal bool Delete(int Id)
    {
      string sql = "DELETE FROM keeps WHERE id = @Id LIMIT 1";
      int deleted = _db.Execute(sql, new { Id });
      return deleted == 1;
    }
  }
}