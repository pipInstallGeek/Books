using Books.Features.BookF.UpdateBook;
using Carter;
using MediatR;

namespace Books.Features.BookF.CreateBook;
public record UpdateBookRequest(int Id, string Title, int Pages);
public record UpdateBookResponse(int Id, string Title, int Pages);

public class UpdateBookEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/books/{id:int}", async (int id, UpdateBookRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest();
            }

            var command = new UpdateBookCommand(request.Id, request.Title, request.Pages);

            var result = await sender.Send(command);

            var response = new UpdateBookResponse(result.Id, result.Title, result.Pages);

            return Results.Ok(response);

        })
        .WithName("UpdateBook")
        .Produces<UpdateBookResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Book")
        .WithDescription("Update Book");
    }
}
