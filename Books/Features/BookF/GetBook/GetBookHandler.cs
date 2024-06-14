﻿using Books.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Books.Features.BookF.GetBook;
public class GetBooksHandler
{
    public record GetBooksQuery() : IRequest<IEnumerable<GetBooksResult>>;

    public record GetBooksResult(int Id, string Title, int Pages);

    internal class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, IEnumerable<GetBooksResult>>
    {
        private readonly AppDbContext _context;

        public GetBooksQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetBooksResult>> Handle(GetBooksQuery query, CancellationToken cancellationToken)
        {
            var books = await _context.Books
                                      .Select(book => new GetBooksResult(book.Id, book.Title, book.Pages))
                                      .ToListAsync(cancellationToken);

            return books;
        }
    }
}
