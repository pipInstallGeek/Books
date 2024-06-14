using Books.CQRS;
using Books.Data;
using Books.Entity;
using Marten;
using Marten.Pagination;

namespace Books.Features.Authors.GetAuthors
{
    public record GetAuthorsQuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetAuthorsResult>;
    public record GetAuthorsResult(IEnumerable<Author> Authors);


    internal class GetAuthorsHandler(IDocumentSession session) : IQueryHandler<GetAuthorsQuery, GetAuthorsResult>

    {
        public async Task<GetAuthorsResult> Handle(GetAuthorsQuery query, CancellationToken cancellationToken)
        {
            var authors = await session.Query<Author>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10, cancellationToken);
            return new GetAuthorsResult(authors);
        }
    }
}
