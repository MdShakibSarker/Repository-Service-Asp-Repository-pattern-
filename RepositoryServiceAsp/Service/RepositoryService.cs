using System.Collections.Generic;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RepositoryServiceAsp.DatabaseContext;

namespace RepositoryServiceAsp.Service;

public class RepositoryService<TEntity,IModel> : IRepositoryService<TEntity, IModel> where TEntity : class, new() where IModel : class
{
	private readonly IMapper _mapper;
	private readonly StudentDbContext dbContext;


	public RepositoryService(IMapper mapper, StudentDbContext dbContext)
    {
		this.dbContext = dbContext;
		_mapper = mapper;
		DbSet= dbContext.Set<TEntity>();
	}
	public DbSet<TEntity> DbSet { get; }
    public async Task<IModel> DeleteAsync(long id, CancellationToken cancellationToken)
	{
		var entity=await DbSet.FindAsync(id);
		if (entity == null)
		{
			return null;
		}
		DbSet.Remove(entity);
		await dbContext.SaveChangesAsync(cancellationToken);
		var deleteModel=_mapper.Map<TEntity,IModel>(entity);
		return deleteModel;
		//var entity = await DbSet.FindAsync(id);
		//if (entity == null)
		//{
		//	return null;
		//}
		//DbSet.Remove(entity);
		//await dbContext.SaveChangesAsync(cancellationToken);
		//var deleteModel = _mapper.Map<TEntity, IModel>(entity);
		//return deleteModel;
	}

	public async Task<IEnumerable<IModel>> GetAllAsync(CancellationToken cancellationToken)
	{
		var entities = await DbSet.ToListAsync(cancellationToken);
		if (entities == null) return null;
		var data = _mapper.Map<IEnumerable<IModel>>(entities);
		return data;
		//var entites = await Dbset.ToListAsync(cancellationToken);
		//if (entites == null) return null;
		//var data = _mapper.Map<IEnumerable<IModel>>(entites);
		//return data;
	}

	public async Task<IModel> GetByIdAsync(long id, CancellationToken cancellationToken)
	{
		var entity = await DbSet.FindAsync(id);
		if (entity == null) return null;
		var model = _mapper.Map<TEntity, IModel>(entity);
		return model;
		//var entity = await Dbset.FindAsync(id);
		//if (entity == null) return null;
		//var model = _mapper.Map<TEntity, IModel>(entity);
		//return model;
	}

	public async Task<IModel> InsertAsync(IModel model, CancellationToken cancellationToken)
	{
		var entity = _mapper.Map<IModel, TEntity>(model);
		DbSet.Add(entity);
		await dbContext.SaveChangesAsync(cancellationToken);
		var insertModel = _mapper.Map<TEntity, IModel>(entity);
		return insertModel;
		//var entity = _mapper.Map<IModel, TEntity>(model);
		//Dbset.Add(entity);
		//await _dbContext.SaveChangesAsync(cancellationToken);
		//var insertModel = _mapper.Map<TEntity, IModel>(entity);
		//return insertModel;
	}

	public async Task<IModel> UpdateAsync(long id, IModel model, CancellationToken cancellationToken)
	{
		var entity = await DbSet.FindAsync(id);
		if (entity == null) return null;
		_mapper.Map(entity, model);
		await dbContext.SaveChangesAsync(cancellationToken);
		var updateModel = _mapper.Map<TEntity,IModel>(entity);
		return updateModel;
		//var entity = await Dbset.FindAsync(id);
		//if (entity == null)
		//{
		//	return null;
		//}
		//_mapper.Map(model, entity);

		//await _dbContext.SaveChangesAsync(cancellationToken);

		//var updatedModel = _mapper.Map<TEntity, IModel>(entity);
		//return updatedModel;
	}
}

