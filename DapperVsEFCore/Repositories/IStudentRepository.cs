using DapperVsEFCore.Entities;

namespace DapperVsEFCore.Repositories
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> EFGetAllAsync();
        Task<Student> EFReadByIdAsync(int studentId);
        Task EFDeleteAsync(int studentId);
        Task EFCreateAsync(Student student);
        Task EFUpdateAsync(Student student);

        Task<IEnumerable<Student>> DapperGetAllAsync();
        Task<Student> DapperReadByIdAsync(int studentId);
        Task DapperDeleteAsync(int studentId);
        Task DapperCreateAsync(Student student);
        Task DapperUpdateAsync(Student student);
    }
}
