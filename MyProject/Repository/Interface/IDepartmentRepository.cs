using MyProject.Model;

namespace MyProject.Repository.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> Get();
        Department Get(int ID);
        int Insert(Department department);
        int Update(Department department);
        int Delete(int ID);
    }
}
