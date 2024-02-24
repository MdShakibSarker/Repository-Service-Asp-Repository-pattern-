using AutoMapper;
using RepositoryServiceAsp.DatabaseContext;
using RepositoryServiceAsp.Models;
using RepositoryServiceAsp.Service;
using RepositoryServiceAsp.ViewModel;

namespace RepositoryServiceAsp.RepositoryService;

public class StudentRepository : RepositoryService<Student, StudentVm>, IStudentRepository
{
	public StudentRepository(IMapper mapper, StudentDbContext dbContext) : base(mapper, dbContext)
	{
	}
}

