using Books.Data;
using MediatR;

using Books.Entity;
using Books.CQRS;
using FluentValidation;


namespace Books.Features.BookF.CreateBook;

public record CreateBookCommand(string Title, int Pages) : ICommand<CreateBookResult>;
public record CreateBookResult(int Id);

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        RuleFor(x => x.Pages).GreaterThan(0).WithMessage("Pages must be greater than 0");
    }
}

internal class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, CreateBookResult>
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



