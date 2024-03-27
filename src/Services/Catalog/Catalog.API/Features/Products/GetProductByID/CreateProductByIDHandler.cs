namespace Catalog.API.Features.Products.GetProductByID
{
    public record GetProductByIDQuery(Guid ID) : IQuery<GetProductByIDResult>;

    public record GetProductByIDResult(Product Product);

    internal class GetProductByIDQueryHandler
        (IDocumentSession session)
        : IQueryHandler<GetProductByIDQuery, GetProductByIDResult>
    {
        public async Task<GetProductByIDResult> Handle(GetProductByIDQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.ID, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(query.ID);
            }

            return new GetProductByIDResult(product);
        }
    }
}
