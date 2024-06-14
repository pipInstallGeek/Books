using Books.Data;
using MediatR;

using Books.Entity;


namespace Books.Features.BookF.CreateBook;

public class CreateBookHandler
{
    public record CreateBookCommand(string Title, int Pages) : IRequest<CreateBookResult>;

    public record CreateBookResult(int Id);

    internal class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, CreateBookResult>
    {
        private readonly AppDbContext _context;

        public CreateBookCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CreateBookResult> Handle(CreateBookCommand command, CancellationToken cancellationToken)
        {
            var book = new Book
            {
                Title = command.Title,
                Pages = command.Pages
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync(cancellationToken);

            return new CreateBookResult(book.Id);
        }
    }
}


