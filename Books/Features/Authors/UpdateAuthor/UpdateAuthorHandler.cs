using System.Windows.Input;
using Books.CQRS;
using Books.Data;
using Books.Entity;
using Microsoft.EntityFrameworkCore;

namespace Books.Features.Authors.UpdateAuthor
{
 
    public record UpdateAuthorCommand(int Id , String Name) :ICommand<UpdateAuthorResult>;
    public record UpdateAuthorResult(bool IsSuccess);
    public class UpdateAuthorCommandHandler(AppDbContext _db) : ICommandHandler<UpdateAuthorCommand, UpdateAuthorResult>
    {
        public async Task<UpdateAuthorResult> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            var author = await _db.Authors.FindAsync(new object[] { command.Id }, cancellationToken);
            
            if (author == null) 
            {
                throw new KeyNotFoundException("Author not found");
            }    

            author.Name = command.Name;
            _db.Update(author);
            await _db.SaveChangesAsync(cancellationToken);

            return new UpdateAuthorResult(true);
        }
    }
}
