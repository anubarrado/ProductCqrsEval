using Application.Products.Create;
using Application.Products.Delete;
using Domain.DomainErrors;
using Domain.Primitives;
using Domain.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.UnitTest.Product.Delete
{
    public class DeleteProductCommandHandlerTest
    {
        private readonly Mock<IProductRepository> _moqProductRepository;
        private readonly Mock<IUnitOfWork> _moqUnitOfWork;
        private readonly DeleteProductCommandHandler _handler;

        public DeleteProductCommandHandlerTest()
        {
            _moqProductRepository = new Mock<IProductRepository>();
            _moqUnitOfWork = new Mock<IUnitOfWork>();
            _handler = new DeleteProductCommandHandler(_moqProductRepository.Object, _moqUnitOfWork.Object);
        }

        [Fact]
        public async Task HandleDeleteProductCommand_WhenIdHasInvalidFormat_ShouldReturnValidationErrorAsync()
        {
            //arrange
            int id = -20;
            DeleteProductCommand command = new DeleteProductCommand(id);

            //act
            var result = await _handler.Handle(command, default);

            //assert
            result.IsError.Should().BeTrue();
            result.FirstError.Type.Should().Be(ErrorType.Validation);
            result.FirstError.Code.Should().Be(ErrorDomain.ErrorProduct.NotExist.Code);
            result.FirstError.Description.Should().Be(ErrorDomain.ErrorProduct.NotExist.Description);
        }
    }
}