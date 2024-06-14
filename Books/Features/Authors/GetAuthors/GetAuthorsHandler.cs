using Books.CQRS;
using Books.Data;
using Books.Entity;
using Microsoft.EntityFrameworkCore;

namespace Books.Features.Authors.GetAuthors
{
    public record GetAuthorsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetAuthorsResult>;
    public record GetAuthorsResult(IEnumerable<Author> Authors);


    internal class GetAuthorsHandler(AppDbContext _db) : IQueryHandler<GetAuthorsQuery, GetAuthorsResult>

    {
        public async Task<GetAuthorsResult> Handle(GetAuthorsQuery query, CancellationToken cancellationToken)
        {
            var authors = await _db.Authors.ToListAsync(cancellationToken);
            return new GetAuthorsResult(authors);
        }
    }
}
