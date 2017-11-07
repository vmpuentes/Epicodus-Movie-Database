using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Library.Models
{
  public class Review
  {
      private int _id;
      private int _moviesId;
      private int _usersId;
      private string _review;

      public Review(int moviesId, int usersId, string review, int id = 0)
      {
        _id = id;
        _moviesId = moviesId;
        _usersId = usersId;
        _review = review;
      }

      public int GetId()
      {
        return _id;
      }

      public int GetMoviesId()
      {
        return _moviesId;
      }

      public int GetUsersId()
      {
        return _usersId;
      }

      public string GetReview()
      {
        return _review;
      }

      public static Review Find(int id)
      {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT * FROM movies_users WHERE id = (@searchId);";

        MySqlParameter searchId = new MySqlParameter();
        searchId.ParameterName = "@searchId";
        searchId.Value = id;
        cmd.Parameters.Add(searchId);

        var rdr = cmd.ExecuteReader() as MySqlDataReader;
        int moviesId = 0;
        int usersId = 0;
        string review = "";

        while(rdr.Read())
        {
          moviesId = rdr.GetInt32(1);
          usersId = rdr.GetInt32(2);
          review = rdr.GetString(3);
        }
        Review newReview = new Review(moviesId, usersId, review);
        conn.Close();
        if (conn != null)
        {
          conn.Dispose();
        }
        return newReview;
      }
  }

}
