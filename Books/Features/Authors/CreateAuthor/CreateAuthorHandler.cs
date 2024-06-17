using System.Windows.Input;
using Books.CQRS;
using Books.Data;
using Books.Entity;

namespace Books.Features.Authors.CreateAuthor
{
    public record CreateAuthorCommand(string Name) :ICommand<CreateAuthorResult>;
    public record CreateAuthorResult(int Id, string Name);

    internal class CreateAuthorCommandHandler(AppDbContext _db) : ICommandHandler<CreateAuthorCommand, CreateAuthorResult>
    {
        public async Task<CreateAuthorResult> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = new Author
            {
                Name = command.Name
            };
            await _db.Authors.AddAsync(author);
            await _db.SaveChangesAsync();
            return new CreateAuthorResult(author.Id, author.Name);
        }
    }
}
