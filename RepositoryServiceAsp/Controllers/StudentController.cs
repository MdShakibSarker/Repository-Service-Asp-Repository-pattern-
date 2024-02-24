using Microsoft.AspNetCore.Mvc;
using RepositoryServiceAsp.RepositoryService;
using RepositoryServiceAsp.ViewModel;

namespace RepositoryServiceAsp.Controllers;

public class StudentController:Controller
{
	private readonly IStudentRepository studentRepository;
	public StudentController(IStudentRepository studentRepository)
	{
		this.studentRepository = studentRepository;
	}
	public async Task<ActionResult<StudentVm>> Index(CancellationToken CancellationToken)
	{
		return View(await studentRepository.GetAllAsync(CancellationToken));
	}
	public async Task<ActionResult<StudentVm>> CreateOrEdit(long id, CancellationToken cancellationToken)
	{
		if (id == 0)
		{
			return View(new StudentVm());
		}
		else
		{
			return View(await studentRepository.GetByIdAsync(id, cancellationToken));
		}
	}
	[HttpPost]

	public async Task<ActionResult<StudentVm>> CreateOrEdit(long id, StudentVm studentVm, CancellationToken cancellationToken)
	{
		if (id == 0)
		{
			if (ModelState.IsValid)
			{
				await studentRepository.InsertAsync(studentVm, cancellationToken);
				return RedirectToAction(nameof(Index));
			}
		}
		else
		{
			await studentRepository.UpdateAsync(id, studentVm, cancellationToken);
			return RedirectToAction(nameof(Index));
		}
		return View(studentVm);
	}
	public async Task<ActionResult<StudentVm>> Delete(long id, CancellationToken cancellationToken)
	{
		if (id == null)
		{
			return RedirectToAction(nameof(Index));
		}
		else
		{
			await studentRepository.DeleteAsync(id, cancellationToken);
			return RedirectToAction(nameof(Index));
		}
	}

	public async Task<ActionResult<StudentVm>> Details(long id, CancellationToken cancellationToken)
	{
		if (id == 0)
		{
			return RedirectToAction(nameof(Index));
		}
		else
		{
			return View(await studentRepository.GetByIdAsync(id, cancellationToken));
		}
	}
}
