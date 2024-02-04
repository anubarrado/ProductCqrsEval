using Application.Products.Create;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            CreateProductCommand command = new CreateProductCommand("Beach chair", "DDDD123456", true, 10, "Multipurpose Beach chair", -20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ErrorDomain.Product.PriceInvalid.Code);
            result.FirstError.Description.Should().Be(ErrorDomain.Product.PriceInvalid.Description);
        }

        [Fact]
        public async Task HandleCreateProductCommand_WhenStockHasInvalidFormat_ShouldReturnValidationErrorAsync()
        {
            //arrange
            CreateProductCommand command = new CreateProductCommand("Beach chair", "DDDD123456", true, -10, "Multipurpose Beach chair", 20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ErrorDomain.Product.StockInvalid.Code);
            result.FirstError.Description.Should().Be(ErrorDomain.Product.StockInvalid.Description);
        }

        [Fact]
        public async Task HandleCreateProductCommand_WhenSkuHasInvalidFormat_ShouldReturnValidationErrorAsync()
        {
            //arrange
            CreateProductCommand command = new CreateProductCommand("Beach chair", "d123456", true, 10, "Multipurpose Beach chair", 20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ErrorDomain.Product.SkuInvalid.Code);
            result.FirstError.Description.Should().Be(ErrorDomain.Product.SkuInvalid.Description);
        }

        [Fact]
        public async Task HandleCreateProductCommand_WhenNameIsEmpty_ShouldReturnValidationErrorAsync()
        {
            //arrange
            CreateProductCommand command = new CreateProductCommand("", "DDDD123456", true, 10, "Multipurpose Beach chair", -20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
        }

        [Fact]
        public async Task HandleCreateProductCommand_WhenDescriptionIsEmpty_ShouldReturnValidationErrorAsync()
        {
            //arrange
            CreateProductCommand command = new CreateProductCommand("Beach chair", "DDDD123456", true, 10, "", -20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
        }

        [Fact]
        public async Task HandleCreateProductCommand_WhenIsOk_ShouldReturnOkAsync()
        {
            //arrange
            CreateProductCommand command = new CreateProductCommand("Beach chair", "DDDD123456", true, 10, "Multipurpose Beach chair", 20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeFalse();
        }
    }
}