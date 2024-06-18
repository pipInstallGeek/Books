using Carter;
using MediatR;

namespace Books.Features.BookF.GetBookById;

public class GetBookByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/getBookById/{id:int}", async (int id, ISender sender) =>
        {
            var result = await sender.Send(new GetBookByIdQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetBookById")
        .Produces<GetBookByIdResult>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status404NotFound)
        .WithSummary("Get Book By Id")
        .WithDescription("Get a book by its ID");
    }
}
