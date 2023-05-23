// <copyright company="ROSEN Swiss AG">
//  Copyright (c) ROSEN Swiss AG
//  This computer program includes confidential, proprietary
//  information and is a trade secret of ROSEN. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of ROSEN. All Rights Reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Marketplace.Core.Model;
using Microsoft.Data.Sqlite;

namespace Marketplace.Dal
{
    //Todo Refactor to each model
    internal class MarketplaceDb : IMarketplaceDb, IDisposable
    {
        private readonly SqliteConnection _connection;

        public MarketplaceDb()
        {
            var path = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @".."));
            _connection = new SqliteConnection($@"Data Source={path}\Marketplace.Dal\marketplace.db");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public async Task<User[]> GetUsersAsync()
        {
            await using var command = new SqliteCommand(
                "SELECT U.Id, U.Username, COUNT(O.Id) AS Offers\r\n" +
                "FROM User U LEFT JOIN Offer O ON U.Id = O.UserId\r\n" +
                "GROUP BY U.Id, U.Username;",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();


                var results = new List<User>();

                while (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? null : reader.GetString(reader.GetOrdinal("Username"))
                    };

                    results.Add(user);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Category[]> GetCategoriesAsync()
        {
            await using var command = new SqliteCommand(
                "SELECT C.Id, C.Name, COUNT(O.Id) AS Offers\r\n" +
                "FROM Category C LEFT JOIN Offer O ON C.Id = O.CategoryId\r\n" +
                "GROUP BY C.Id, C.Name;",
                _connection);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();


                var results = new List<Category>();

                while (await reader.ReadAsync())
                {
                    var category = new Category
                    {
                        Id = reader.GetByte(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name"))
                    };

                    results.Add(category);
                }

                return results.ToArray();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
    
        public async Task<Offer[]> GetOffersAsync(int pageNumber, int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;

            await using var command = new SqliteCommand(
                "SELECT O.Id, O.Title, O.Description, O.PublishedOn, O.Location, O.PictureUrl, " +
                "       U.Id AS UserId, U.Username AS UserUsername, " +
                "       C.Id AS CategoryId, C.Name AS CategoryName " +
                "FROM Offer O " +
                "LEFT JOIN User U ON O.UserId = U.Id " +
                "LEFT JOIN Category C ON O.CategoryId = C.Id " +
                "ORDER BY O.PublishedOn DESC " +
                "LIMIT @PageSize OFFSET @Skip;",
                _connection);

            command.Parameters.AddWithValue("@PageSize", pageSize);
            command.Parameters.AddWithValue("@Skip", skip);

            try {

                await using var reader = await command.ExecuteReaderAsync();

                var results = new List<Offer>();

                while (await reader.ReadAsync())
                {
                    var offer = new Offer
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        PublishedOn = reader.GetDateTime(reader.GetOrdinal("PublishedOn")),
                        Location = reader.GetString(reader.GetOrdinal("Location")),
                        PictureUrl = reader.GetString(reader.GetOrdinal("PictureUrl")),
                        User = new User
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("UserId")),
                            Username = reader.GetString(reader.GetOrdinal("UserUsername"))
                        },
                        Category = new Category
                        {
                            Id = reader.GetByte(reader.GetOrdinal("CategoryId")),
                            Name = reader.GetString(reader.GetOrdinal("CategoryName"))
                        }
                    };

                    results.Add(offer);
                }

                return results.ToArray();

            } catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<int> GetTotalOfferCountAsync()
        {
            await using var command = new SqliteCommand("SELECT COUNT(*) FROM Offer", _connection);

            try
            {
                var result = await command.ExecuteScalarAsync();
                return Convert.ToInt32(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            await using var command = new SqliteCommand(
                "SELECT Id, Username " +
                "FROM User " +
                "WHERE Id = @Id;",
                _connection);

            command.Parameters.AddWithValue("@Id", userId);

            try
            {
                await using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    var user = new User
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.IsDBNull(reader.GetOrdinal("Username")) ? null : reader.GetString(reader.GetOrdinal("Username"))
                    };

                    return user;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return null;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await using var command = new SqliteCommand(
                "INSERT INTO User (Id, Username) " +
                "VALUES (@Id, @Username);",
                _connection);

            command.Parameters.AddWithValue("@Id", user.Id);
            command.Parameters.AddWithValue("@Username", user.Username != null ? user.Username: DBNull.Value);

            try
            {
                await command.ExecuteNonQueryAsync();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<Offer> CreateOfferAsync(Offer offer)
        {
            await using var command = new SqliteCommand(
                "INSERT INTO Offer (Id, CategoryId, Description, Location, PictureUrl, PublishedOn, Title, UserId) " +
                "VALUES (@Id, @CategoryId, @Description, @Location, @PictureUrl, @PublishedOn, @Title, @UserId);",
                _connection);

            command.Parameters.AddWithValue("@Id", offer.Id);
            command.Parameters.AddWithValue("@CategoryId", offer.CategoryId);
            command.Parameters.AddWithValue("@Description", offer.Description);
            command.Parameters.AddWithValue("@Location", offer.Location);
            command.Parameters.AddWithValue("@PictureUrl", offer.PictureUrl);
            command.Parameters.AddWithValue("@PublishedOn", offer.PublishedOn);
            command.Parameters.AddWithValue("@Title", offer.Title);
            command.Parameters.AddWithValue("@UserId", offer.UserId);

            try
            {
                await command.ExecuteNonQueryAsync();
                return offer;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
