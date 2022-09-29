
//using Business.Handlers.WareHouses.Queries;
//using DataAccess.Abstract;
//using Moq;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq.Expressions;
//using System.Threading.Tasks;
//using static Business.Handlers.WareHouses.Queries.GetWareHouseQuery;
//using Entities.Concrete;
//using static Business.Handlers.WareHouses.Queries.GetWareHousesQuery;
//using static Business.Handlers.WareHouses.Commands.CreateWareHouseCommand;
//using Business.Handlers.WareHouses.Commands;
//using Business.Constants;
//using static Business.Handlers.WareHouses.Commands.UpdateWareHouseCommand;
//using static Business.Handlers.WareHouses.Commands.DeleteWareHouseCommand;
//using MediatR;
//using System.Linq;
//using FluentAssertions;


//namespace Tests.Business.HandlersTest
//{
//	[TestFixture]
//	public class WareHouseHandlerTests
//	{
//		Mock<IWareHouseRepository> _wareHouseRepository;
//		Mock<IMediator> _mediator;
//		[SetUp]
//		public void Setup()
//		{
//			_wareHouseRepository = new Mock<IWareHouseRepository>();
//			_mediator = new Mock<IMediator>();
//		}

//		[Test]
//		public async Task WareHouse_GetQuery_Success()
//		{
//			//Arrange
//			var query = new GetWareHouseQuery();

//			_wareHouseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WareHouse, bool>>>())).ReturnsAsync(new WareHouse()
////propertyler buraya yazılacak
////{																		
////WareHouseId = 1,
////WareHouseName = "Test"
////}
//);

//			var handler = new GetWareHouseQueryHandler(_wareHouseRepository.Object, _mediator.Object);

//			//Act
//			var x = await handler.Handle(query, new System.Threading.CancellationToken());

//			//Asset
//			x.Success.Should().BeTrue();
//			//x.Data.WareHouseId.Should().Be(1);

//		}

//		[Test]
//		public async Task WareHouse_GetQueries_Success()
//		{
//			//Arrange
//			var query = new GetWareHousesQuery();

//			_wareHouseRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<WareHouse, bool>>>()))
//						.ReturnsAsync(new List<WareHouse> { new WareHouse() { /*TODO:propertyler buraya yazılacak WareHouseId = 1, WareHouseName = "test"*/ } });

//			var handler = new GetWareHousesQueryHandler(_wareHouseRepository.Object, _mediator.Object);

//			//Act
//			var x = await handler.Handle(query, new System.Threading.CancellationToken());

//			//Asset
//			x.Success.Should().BeTrue();
//			((List<WareHouse>)x.Data).Count.Should().BeGreaterThan(1);

//		}

//		[Test]
//		public async Task WareHouse_CreateCommand_Success()
//		{
//			WareHouse rt = null;
//			//Arrange
//			var command = new CreateWareHouseCommand();
//			//propertyler buraya yazılacak
//			//command.WareHouseName = "deneme";

//			_wareHouseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WareHouse, bool>>>()))
//						.ReturnsAsync(rt);

//			_wareHouseRepository.Setup(x => x.Add(It.IsAny<WareHouse>())).Returns(new WareHouse());

//			var handler = new CreateWareHouseCommandHandler(_wareHouseRepository.Object, _mediator.Object);
//			var x = await handler.Handle(command, new System.Threading.CancellationToken());

//			_wareHouseRepository.Verify(x => x.SaveChangesAsync());
//			x.Success.Should().BeTrue();
//			x.Message.Should().Be(Messages.Added);
//		}

//		[Test]
//		public async Task WareHouse_CreateCommand_NameAlreadyExist()
//		{
//			//Arrange
//			var command = new CreateWareHouseCommand();
//			//propertyler buraya yazılacak 
//			//command.WareHouseName = "test";

//			_wareHouseRepository.Setup(x => x.Query())
//										   .Returns(new List<WareHouse> { new WareHouse() { /*TODO:propertyler buraya yazılacak WareHouseId = 1, WareHouseName = "test"*/ } }.AsQueryable());

//			_wareHouseRepository.Setup(x => x.Add(It.IsAny<WareHouse>())).Returns(new WareHouse());

//			var handler = new CreateWareHouseCommandHandler(_wareHouseRepository.Object, _mediator.Object);
//			var x = await handler.Handle(command, new System.Threading.CancellationToken());

//			x.Success.Should().BeFalse();
//			x.Message.Should().Be(Messages.NameAlreadyExist);
//		}

//		[Test]
//		public async Task WareHouse_UpdateCommand_Success()
//		{
//			//Arrange
//			var command = new UpdateWareHouseCommand();
//			//command.WareHouseName = "test";

//			_wareHouseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WareHouse, bool>>>()))
//						.ReturnsAsync(new WareHouse() { /*TODO:propertyler buraya yazılacak WareHouseId = 1, WareHouseName = "deneme"*/ });

//			_wareHouseRepository.Setup(x => x.Update(It.IsAny<WareHouse>())).Returns(new WareHouse());

//			var handler = new UpdateWareHouseCommandHandler(_wareHouseRepository.Object, _mediator.Object);
//			var x = await handler.Handle(command, new System.Threading.CancellationToken());

//			_wareHouseRepository.Verify(x => x.SaveChangesAsync());
//			x.Success.Should().BeTrue();
//			x.Message.Should().Be(Messages.Updated);
//		}

//		[Test]
//		public async Task WareHouse_DeleteCommand_Success()
//		{
//			//Arrange
//			var command = new DeleteWareHouseCommand();

//			_wareHouseRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<WareHouse, bool>>>()))
//						.ReturnsAsync(new WareHouse() { /*TODO:propertyler buraya yazılacak WareHouseId = 1, WareHouseName = "deneme"*/});

//			_wareHouseRepository.Setup(x => x.Delete(It.IsAny<WareHouse>()));

//			var handler = new DeleteWareHouseCommandHandler(_wareHouseRepository.Object, _mediator.Object);
//			var x = await handler.Handle(command, new System.Threading.CancellationToken());

//			_wareHouseRepository.Verify(x => x.SaveChangesAsync());
//			x.Success.Should().BeTrue();
//			x.Message.Should().Be(Messages.Deleted);
//		}
//	}
//}

