using AutoMapper;
using RepositoryServiceAsp.Models;

namespace RepositoryServiceAsp.ViewModel;
[AutoMap(typeof(Student),ReverseMap =true)]
public class StudentVm
{
	public long Id { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string Phone { get; set;}
}
