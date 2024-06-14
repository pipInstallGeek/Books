using Books.CQRS;
using Books.Data;
using FluentValidation;
using MediatR;

namespace Books.Features.BookF.UpdateBook;
public record UpdateBookCommand(int Id, string Title, int Pages) : ICommand<UpdateBookResult>;
public record UpdateBookResult(int Id, string Title, int Pages);

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Pages).GreaterThan(0).WithMessage("Pages must be greater than 0");
    }
}

internal class UpdateBookCommandHandler : ICommandHandler<UpdateBookCommand, UpdateBookResult>
{
    private readonly AppDbContext _context;

    public UpdateBookCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UpdateBookResult> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(new object[] { command.Id }, cancellationToken);

        if (book == null)
        {
            throw new KeyNotFoundException("Book not found.");
        }

        book.Title = command.Title;
        book.Pages = command.Pages;

        await _context.SaveChangesAsync(cancellationToken);

        return new UpdateBookResult(book.Id, book.Title, book.Pages);
    }
}

