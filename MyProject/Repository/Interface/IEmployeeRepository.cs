using MyProject.Model;

namespace MyProject.Repository.Interface
{
    public interface IEmployeeRepository
    {
        //Get All data
        IEnumerable<Employee> Get();
        Employee Get(string NIK); //Get data by NIK
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(string NIK);

        //Department
        //IEnumerable<Department> Get_Department();
        //Department Get_DepartmentID(int Id);
        //int Insert(Department department);
        //int Update(Department department);
        //int Delete(int ID);
    }
}
