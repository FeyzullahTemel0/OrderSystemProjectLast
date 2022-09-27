
using Business.Handlers.WareHouses.Commands;
using Business.Handlers.WareHouses.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Entities.Dtos;

namespace WebAPI.Controllers
{
	/// <summary>
	/// WareHouses If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class WareHousesController : BaseApiController
	{
		///<summary>
		///List WareHouses
		///</summary>
		///<remarks>WareHouses</remarks>
		///<return>List WareHouses</return>
		///<response code="200"></response>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WareHouse>))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getall")]
		public async Task<IActionResult> GetList()
		{
			var result = await Mediator.Send(new GetWareHousesQuery());
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}


		// açıklamayı da düzeltilecek gerçek projelerde.
        ///<summary>
        ///List WareHouseDto
        ///</summary>
        ///<remarks>WareHouseDto</remarks>
        ///<return>List WareHouseDto</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WareHouseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getallwarehouse")]
        public async Task<IActionResult> GetAllWareHouseDto()
        {
            var result = await Mediator.Send(new GetAllWareHouseDtoQuery()); //***
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

		//------------------------------------------------------------------------------------------




        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>WareHouses</remarks>
        ///<return>WareHouses List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WareHouse))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpGet("getbyid")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await Mediator.Send(new GetWareHouseQuery { Id = id });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Add WareHouse.
		/// </summary>
		/// <param name="createWareHouse"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateWareHouseCommand createWareHouse)
		{
			var result = await Mediator.Send(createWareHouse);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Update WareHouse.
		/// </summary>
		/// <param name="updateWareHouse"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateWareHouseCommand updateWareHouse)
		{
			var result = await Mediator.Send(updateWareHouse);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Delete WareHouse.
		/// </summary>
		/// <param name="deleteWareHouse"></param>
		/// <returns></returns>
		[Produces("application/json", "text/plain")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteWareHouseCommand deleteWareHouse)
		{
			var result = await Mediator.Send(deleteWareHouse);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
