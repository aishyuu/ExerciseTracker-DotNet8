using ExerciseTracker_DotNet8.Data;
using ExerciseTracker_DotNet8.Models;
using ExerciseTracker_DotNet8.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace ExerciseTracker_DotNet8.Repository.Services
{
    public class ExerciseLogRepository : IExerciseLogRepository
    {
        private readonly AppDbContext appDbContext;

        public ExerciseLogRepository (AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<ExerciseLog>> GetAllLogs()
        {
            return await appDbContext.ExerciseLog.ToListAsync();
        }

        public async Task<ExerciseLog> GetLog(int id)
        {
            return await appDbContext.ExerciseLog.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<ExerciseLog> AddLog(ExerciseLog log)
        {
            var result = await appDbContext.ExerciseLog.AddAsync(log);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<ExerciseLog> UpdateLog(ExerciseLog log)
        {
            var result = await appDbContext.ExerciseLog.FirstOrDefaultAsync(e => e.Id == log.Id);

            if (result != null)
            {
                result.DateStart = log.DateStart;
                result.DateEnd = log.DateEnd;
                result.Duration = log.Duration;
                result.Comments = log.Comments;

                await appDbContext.SaveChangesAsync();
                return result;
            }

            return null;
        }

        public async Task<ExerciseLog> DeleteLog(int id)
        {
            var result = await appDbContext.ExerciseLog.FirstOrDefaultAsync(e => e.Id == id);
            if (result != null)
            {
                appDbContext.ExerciseLog.Remove(result);
                await appDbContext.SaveChangesAsync();
                return null;
            }
            return null;
        }
    }
}
