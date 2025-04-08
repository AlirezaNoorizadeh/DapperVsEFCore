using Dapper;
using DapperVsEFCore.Entities;
using DapperVsEFCore.Persistence;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DapperVsEFCore.Repositories
{
    public class StudentRepository(AppDbContext dbContext) : IStudentRepository
    {
        #region Dapper
        public async Task DapperCreateAsync(Student student)
        {
            using var connection = new SqlConnection(dbContext.Database.GetConnectionString());

            string insertQuery = """
                INSERT INTO Student (FirstName,LastName,Age)
                VALUES (@FirstName,@LastName,@Age)
                """;

            await connection.ExecuteAsync(insertQuery, student);
        }

        public async Task DapperDeleteAsync(int studentId)
        {
            using var connection = new SqlConnection(dbContext.Database.GetConnectionString());

            string deleteQuery = $"""
                DELETE Student
                WHERE Id={studentId}
                """;

            var affectedRow = await connection.ExecuteAsync(deleteQuery);

            if (affectedRow == 0)
                throw new BadHttpRequestException("Student is invalid");
        }

        public async Task<Student> DapperReadByIdAsync(int studentId)
        {
            using var connection = new SqlConnection(dbContext.Database.GetConnectionString());

            string getQuery = $"""
                SELECT * FROM Student
                WHERE Id={studentId}
                """;

            var student = await connection.QueryFirstOrDefaultAsync<Student>(getQuery);

            return student is not null ? student : throw new BadHttpRequestException("StudentId is invalid");
        }

        public async Task<IEnumerable<Student>> DapperGetAllAsync()
        {
            using var connection = new SqlConnection(dbContext.Database.GetConnectionString());

            string getQuery = $"""
                SELECT * FROM Student
                """;

            return await connection.QueryAsync<Student>(getQuery);
        }

        public async Task DapperUpdateAsync(Student student)
        {
            using var connection = new SqlConnection(dbContext.Database.GetConnectionString());

            string updateQuery = $"""
                UPDATE Student
                SET FirstName=@FirstName,LastName=@LastName,Age=@Age
                WHERE Id=@Id
                """;

            var affectedRow = await connection.ExecuteAsync(updateQuery,student);

            if (affectedRow == 0)
                throw new BadHttpRequestException("Student is invalid");
        }
        #endregion

        #region EFCore
        public async Task EFCreateAsync(Student student)
        {
            student.Id = 0;
            dbContext.Students.Add(student);
            await dbContext.SaveChangesAsync();
        }

        public async Task EFDeleteAsync(int studentId)
        {
            if (!await dbContext.Students.AnyAsync(d => d.Id == studentId))
            {
                throw new BadHttpRequestException("StudentId is invalid");
            }
            await dbContext.Students
                .Where(d => d.Id == studentId)
                  .ExecuteDeleteAsync();
        }

        public async Task<Student> EFReadByIdAsync(int studentId)
        {
            if (!await dbContext.Students.AnyAsync(d => d.Id == studentId))
            {
                throw new BadHttpRequestException("StudentId is invalid");
            }

            return await dbContext.Students
                .AsNoTracking()
                .FirstAsync(d => d.Id == studentId);
        }

        public async Task<IEnumerable<Student>> EFGetAllAsync()
        {
            return await dbContext.Students
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task EFUpdateAsync(Student student)
        {
            if (!await dbContext.Students.AnyAsync(d => d.Id == student.Id))
            {
                throw new BadHttpRequestException("Student is invalid");
            }

            dbContext.Students.Update(student);

            await dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
