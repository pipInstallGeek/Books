using Books.Features.Authors.UpdateAuthor;
using Carter;
using Mapster;
using MediatR;

namespace Books.Features.Authors.CreateAuthor
{

    public record UpdateAuthorRequest(int Id, string Name);
    public record UpdateAuthorResponse(bool IsSuccess);
    public class UpdateAuthorEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/authors", async (UpdateAuthorRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateAuthorCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateAuthorResponse>();
                
                return Results.Ok(response);
            })
             .WithName("UpdateProduct")
            .Produces<UpdateAuthorResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update Product")
            .WithDescription("Update Product");
        }
    }
}
        