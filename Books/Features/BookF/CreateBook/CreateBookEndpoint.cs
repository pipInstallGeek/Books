using Carter;
using MediatR;
using static Books.Features.BookF.CreateBook.CreateBookHandler;

namespace Books.Features.BookF.CreateBook
{
    public class CreateBookEndpoint : CarterModule
    {
        

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/books", async (CreateBookHandler.CreateBookCommand command, IMediator mediator) =>
            {
                /*var validationResult = new CreateBookCommandValidator().Validate(command);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }
*/
                var result = await mediator.Send(command);
                return Results.Created($"/books/{result.Id}", result);
            });
        }
    }
}
