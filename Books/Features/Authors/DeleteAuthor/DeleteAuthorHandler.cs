using System.Windows.Input;
using Books.CQRS;
using Books.Data;

namespace Books.Features.Authors.DeleteAuthor
{
    public record DeleteAuthorCommand(int Id) : ICommand<DeleteAuthortResult>;

    public record DeleteAuthortResult(bool IsSuccess);


    public class DeleteAuthorCommandHandler(AppDbContext _db) : ICommandHandler<DeleteAuthorCommand, DeleteAuthortResult>
    {
        public async Task<DeleteAuthortResult> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = _db.Authors.FirstOrDefault(x => x.Id == command.Id);
            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();

            return new DeleteAuthortResult(true);
        }
    }
}
