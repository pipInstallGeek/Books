using Books.Features.Authors.DeleteAuthor;
using Carter;
using Mapster;
using MediatR;

namespace Books.Features.Authors.CreateAuthor
{

    public record DeleteAuthorResponse(bool IsSuccess);

    public class DeleteAuthorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/authors/{id}", async (int id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteAuthorCommand(id));

                var response = result.Adapt<DeleteAuthorResponse>();

                return Results.Ok(response);
            })
                .WithName("DeleteAuthor")
                .Produces<DeleteAuthorResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("Delete Product")
                .WithDescription("Delete Product");
        }
    }
}
