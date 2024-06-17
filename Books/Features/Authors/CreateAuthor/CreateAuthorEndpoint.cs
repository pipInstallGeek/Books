using Carter;
using Mapster;
using MediatR;

namespace Books.Features.Authors.CreateAuthor
{
    public record CreateAuthorRequest( string Name);
    public record CreateAuthorResponse(int Id, string Name);
    public class CreateAuthorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/authors", async (CreateAuthorRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateAuthorCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateAuthorResponse>();
                return Results.Created($"/authors/{response.Id}", response);


            }).WithName("CreateAuthor")
            .Produces<CreateAuthorResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Author");
        }
    }
}
