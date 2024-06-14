using Books.CQRS;
using Books.Data;
using FluentValidation;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Books.Features.BookF.DeleteBook;
public record DeleteBookCommand(int Id) : ICommand<DeleteBookResult>;
public record DeleteBookResult(bool IsSuccess);

public class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
{
    public DeleteBookCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Book ID is required");
    }
}

internal class DeleteBookCommandHandler : ICommandHandler<DeleteBookCommand, DeleteBookResult>
{
    private readonly AppDbContext _context;

    public DeleteBookCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<DeleteBookResult> Handle(DeleteBookCommand command, CancellationToken cancellationToken)
    {
        var book = await _context.Books.FindAsync(new object[] { command.Id }, cancellationToken);

        if (book == null)
        {
            return new DeleteBookResult(false);
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync(cancellationToken);

        return new DeleteBookResult(true);
    }
}

