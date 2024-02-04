using Application.Products.Create;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Products;

namespace Application.UnitTest.Product.Create
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _moqProductRepository;
        private readonly Mock<IUnitOfWork> _moqUnitOfWork;
        private readonly CreateProductCommandHandler _handler;


        public CreateProductCommandHandlerTest()
        {
            _moqProductRepository = new Mock<IProductRepository>();
            _moqUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new CreateProductCommandHandler(_moqProductRepository.Object, _moqUnitOfWork.Object);
        }

        [Fact]
        public async Task HandleCreateProductCommand_WhenPriceHasInvalidFormat_ShouldReturnValidationErrorAsync()
        {
            //arrange
            CreateProductCommand command = new CreateProductCommand("Silla Playera", "PQ123", true, 20, "Silla playera", -20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ErrorDomain.Product.PriceInvalid.Code);
            result.FirstError.Description.Should().Be(ErrorDomain.Product.PriceInvalid.Description);
        }
    }
}