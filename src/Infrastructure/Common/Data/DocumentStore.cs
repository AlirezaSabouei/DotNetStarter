using System.Linq.Expressions;
using Application.Common.Data;
using Domain;
using MediatR;
using MongoDB.Driver;

namespace Infrastructure.Common.Data;

public class DocumentStore<TEntity>(IMongoDatabase database, IMediator mediator)
    : IDocumentStore<TEntity> where TEntity : BaseEntity
{
    private readonly IMongoCollection<TEntity> _collection =
        database.GetCollection<TEntity>(typeof(TEntity).Name);

    public async Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter,
        CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _collection
            .Find(entity => entity.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task InsertAsync(TEntity entity)
    {
        entity.CreateDate = DateTimeOffset.UtcNow;
        entity.UpdateDate = DateTimeOffset.UtcNow;

        await _collection.InsertOneAsync(entity);
        await DispatchEvents(entity);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        entity.UpdateDate = DateTimeOffset.UtcNow;

        await _collection.ReplaceOneAsync(
            existing => existing.Id == entity.Id,
            entity);

        await DispatchEvents(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _collection.DeleteOneAsync(entity => entity.Id == id);
    }

    private async Task DispatchEvents(TEntity entity)
    {
        foreach (var domainEvent in entity.DomainEvents)
        {
            await mediator.Publish(domainEvent);
        }

        entity.ClearDomainEvents();
    }
}
