using Books.Features.BookF.GetBook;
using Carter;
using MediatR;

namespace Books.Features.BookF.CreateBook;
public class GetBooksEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/getBooks", async (ISender sender) =>
        {
            var result = await sender.Send(new GetBooksQuery());
            return Results.Ok(result);
        })
        .WithName("GetBooks")
        .Produces<IEnumerable<GetBooksResult>>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Books")
        .WithDescription("Get all books");
    }
}

