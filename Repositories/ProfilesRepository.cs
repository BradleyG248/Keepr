using System;
using System.Collections.Generic;
using System.Data;
using Keepr.Models;
using Dapper;

namespace Keepr.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile Get(string UserId)
    {
      string sql = "SELECT * FROM profiles WHERE userId = @UserId";
      Profile profile = _db.QueryFirstOrDefault<Profile>(sql, new { UserId });
      return profile;
    }

    internal Profile Create(Profile ProfileData)
    {
      string sql = @"
            INSERT INTO profiles
            (name, email, img, userId)
            VALUES
            (@Name, @Email, @Img, @UserId);
            SELECT LAST_INSERT_ID();
            ";
      _db.Execute(sql, ProfileData);
      return ProfileData;
    }
    internal void Edit(Profile EditedProfile)
    {
      string sql = @"
        UPDATE profiles
        SET
            name = @Name,
            img = @Img,
            email = @Email
        WHERE userId = @UserId;
        ";
      _db.Execute(sql, EditedProfile);
    }
    internal bool Delete(string UserId)
    {
      string sql = "DELETE FROM profiles WHERE userId = @UserId LIMIT 1";
      int deleted = _db.Execute(sql, new { UserId });
      return deleted == 1;
    }
  }
}