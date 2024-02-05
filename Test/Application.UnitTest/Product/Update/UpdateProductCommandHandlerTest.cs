using Application.Products.Create;
using Application.Products.Update;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.UnitTest.Product.Update
{
    public class UpdateProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _moqProductRepository;
        private readonly Mock<IUnitOfWork> _moqUnitOfWork;
        private readonly UpdateProductCommandHandler _handler;

        private readonly int id = 123;

        public UpdateProductCommandHandlerTest()
        {
            _moqProductRepository = new Mock<IProductRepository>();
            _moqUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new UpdateProductCommandHandler(_moqProductRepository.Object, _moqUnitOfWork.Object);
        }

        [Fact]
        public async Task HandleUpdateProductCommand_WhenPriceHasInvalidFormat_ShouldReturnValidationErrorAsync()
        {
            //arrange
            UpdateProductCommand command = new UpdateProductCommand(id, "Beach chair", "DDDD023456", true, 10, "Multipurpose Beach chair", -20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ErrorDomain.ErrorProduct.PriceInvalid.Code);
            result.FirstError.Description.Should().Be(ErrorDomain.ErrorProduct.PriceInvalid.Description);
        }

        [Fact]
        public async Task HandleUpdateProductCommand_WhenStockHasInvalidFormat_ShouldReturnValidationErrorAsync()
        {
            //arrange
            UpdateProductCommand command = new UpdateProductCommand(id, "Beach chair", "DDDD023456", true, -10, "Multipurpose Beach chair", 20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ErrorDomain.ErrorProduct.StockInvalid.Code);
            result.FirstError.Description.Should().Be(ErrorDomain.ErrorProduct.StockInvalid.Description);
        }

        [Fact]
        public async Task HandleUpdateProductCommand_WhenSkuHasInvalidFormat_ShouldReturnValidationErrorAsync()
        {
            //arrange
            UpdateProductCommand command = new UpdateProductCommand(id,"Beach chair", "d123456", true, 10, "Multipurpose Beach chair", 20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ErrorDomain.ErrorProduct.SkuInvalid.Code);
            result.FirstError.Description.Should().Be(ErrorDomain.ErrorProduct.SkuInvalid.Description);
        }

        [Fact]
        public async Task HandleUpdateProductCommand_WhenNameIsEmpty_ShouldReturnValidationErrorAsync()
        {
            //arrange
            UpdateProductCommand command = new UpdateProductCommand(id, "", "DDDD023456", true, 10, "Multipurpose Beach chair", -20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
        }

        [Fact]
        public async Task HandleUpdateProductCommand_WhenDescriptionIsEmpty_ShouldReturnValidationErrorAsync()
        {
            //arrange
            UpdateProductCommand command = new UpdateProductCommand(id, "Beach chair", "DDDD023456", true, 10, "", -20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
        }

        [Fact]
        public async Task HandleUpdateProductCommand_WhenIsOk_ShouldReturnOkAsync()
        {
            //arrange
            UpdateProductCommand command = new UpdateProductCommand(id, "Beach chair", "DDDD023456", true, 10, "Multipurpose Beach chair", 20);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeFalse();
        }
    }
}