using Books.Features.BookF.DeleteBook;
using Carter;
using MediatR;

namespace Books.Features.BookF.CreateBook;
public class DeleteBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/books/{id:int}", async (int id, ISender sender) =>
        {
            var command = new DeleteBookCommand(id);

            var result = await sender.Send(command);

            return result.IsSuccess ? Results.NoContent() : Results.NotFound();

        })
        .WithName("DeleteBook")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Book")
        .WithDescription("Delete Book");
    }
}
