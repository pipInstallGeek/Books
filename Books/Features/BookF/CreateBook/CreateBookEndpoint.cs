using Carter;
using MediatR;
namespace Books.Features.BookF.CreateBook
{
    public record CreateBookRequest(string Title, int Pages);
    public record CreateBookResponse(int Id);

    public class CreateBookEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/createBooks",
                async (CreateBookRequest request, ISender sender) =>
                {
                    var command = new CreateBookCommand(request.Title, request.Pages);

                    var result = await sender.Send(command);

                    var response = new CreateBookResponse(result.Id);

                    return Results.Created($"/books/{response.Id}", response);

                })
            .WithName("CreateBook")
            .Produces<CreateBookResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Book")
            .WithDescription("Create Book");
        }
    }

}
