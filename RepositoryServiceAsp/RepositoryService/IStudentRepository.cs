using RepositoryServiceAsp.Models;
using RepositoryServiceAsp.Service;
using RepositoryServiceAsp.ViewModel;

namespace RepositoryServiceAsp.RepositoryService;

public interface IStudentRepository:IRepositoryService<Student ,StudentVm>
{

}
