using ExerciseTracker_DotNet8.Models;

namespace ExerciseTracker_DotNet8.Repository.Interface
{
    public interface IExerciseLogRepository
    {
        Task<List<ExerciseLog>> GetAllLogs();
        Task<ExerciseLog> GetLog(int id);
        Task<ExerciseLog> AddLog(ExerciseLog log);
        Task<ExerciseLog> UpdateLog(ExerciseLog log);
        Task<ExerciseLog> DeleteLog (int id);
    }
}
