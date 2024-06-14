using Books.Features.BookF.GetBook;
using Carter;
using MediatR;

namespace Books.Features.BookF.CreateBook
{
    public class GetBooksEndpoint : CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/books", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetBooksHandler.GetBooksQuery());
                return Results.Ok(result);
            });
        }
    }

}
