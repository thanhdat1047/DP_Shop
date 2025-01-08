using DP_Shop.Data.Entities;
using DP_Shop.Services;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DP_Shop.Data
{
   
    public class AddressDataSeeder
    {
        private readonly AppDbContext _dbContext;
        public AddressDataSeeder(AppDbContext dbContext)
        {
            _dbContext  = dbContext;
        }

        public async void SeedProvinces(string folderPath)
        {
            if(!_dbContext.Provinces.Any())
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var provinces = FileReader.ReadJsonFiles<Provinces>(folderPath);
                    _dbContext.Provinces.AddRange(provinces);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async void SeedDistricts(string folderPath)
        {
            if (!_dbContext.Districts.Any())
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var districts = FileReader.ReadJsonFiles<District>(folderPath);
                    _dbContext.Districts.AddRange(districts);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch 
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public async void SeedWards(string folderPath)
        {
            if (!_dbContext.Wards.Any())
            {
                using var transaction = await _dbContext.Database.BeginTransactionAsync();
                try
                {
                    var wards = FileReader.ReadJsonFiles<Ward>(folderPath);
                    _dbContext.Wards.AddRange(wards);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }

}
