using Books.Entity;
using Books.Features.Authors.GetAuthors;
using Carter;
using Mapster;
using MediatR;

namespace Books.Features.Authors.CreateAuthors
{
    public record GetAuthorsRequest(int? PageNumber =1, int? PageSize =10);
    public record GetAuthorsResponse(IEnumerable<Author> Authors);
    public class GetAuthorsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/authors", async ([AsParameters] GetAuthorsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetAuthorsQuery>();
                var result = await sender.Send(query);
                var response = result.Adapt<GetAuthorsResponse>();
                return Results.Ok(response);
            });
        }
    }
}

