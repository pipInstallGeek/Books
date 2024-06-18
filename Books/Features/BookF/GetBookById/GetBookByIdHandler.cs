using Books.CQRS;
using Books.Data;
using Microsoft.EntityFrameworkCore;

namespace Books.Features.BookF.GetBookById;
public record GetBookByIdQuery(int Id) : IQuery<GetBookByIdResult>;
public record GetBookByIdResult(int Id, string Title, int Pages);

internal class GetBookByIdQueryHandler : IQueryHandler<GetBookByIdQuery, GetBookByIdResult>
{
    private readonly AppDbContext _context;

    public GetBookByIdQueryHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GetBookByIdResult> Handle(GetBookByIdQuery query, CancellationToken cancellationToken)
    {
        var book = await _context.Books
                                 .Where(b => b.Id == query.Id)
                                 .Select(book => new GetBookByIdResult(book.Id, book.Title, book.Pages))
                                 .FirstOrDefaultAsync(cancellationToken);

        return book;
    }
}
