using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests;
using Sabio.Services;
using Sabio.Services.Interfaces;
using Sabio.Web.Controllers;
using Sabio.Web.Models.Responses;
using System;

namespace Sabio.Web.Api.Controllers
{
    [Route("api/pokedex")]
    [ApiController]
    public class PokemonApiController : BaseApiController
    {
        private IPokemonService _pService = null;
        private IAuthenticationService<int> _authService = null;

        public PokemonApiController(IPokemonService pService, IAuthenticationService<int> authService,
            ILogger<PokemonApiController> logger) : base(logger)
        {
            _pService = pService;
            _authService = authService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<ItemResponse<Paged<Pokemon>>> GetPaginated(int pageIndex, int pageSize)
        {
            ActionResult result = null;

            try
            {
                Paged<Pokemon> page = _pService.GetPaginated(pageIndex, pageSize);

                if (page == null)
                {
                    result = NotFound404(new ErrorResponse("Records Not Found"));
                }
                else
                {
                    ItemResponse<Paged<Pokemon>> response = new ItemResponse<Paged<Pokemon>>();
                    response.Item = page;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public ActionResult<ItemResponse<Pokemon>> GetPokemonById(int id)
        {
            ActionResult result = null;

            try
            {
                Pokemon pokemon = _pService.GetPokemonById(id);
                if (pokemon == null)
                {
                    result = NotFound404(new ErrorResponse("Pokemon Not Found"));
                }
                else
                {
                    ItemResponse<Pokemon> response = new ItemResponse<Pokemon>();
                    response.Item = pokemon;
                    result = Ok200(response);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                result = StatusCode(500, new ErrorResponse(ex.Message.ToString()));
            }
            return result;
        }

        [HttpPost]
        public ActionResult<ItemResponse<int>> Add(PokemonAddRequest model)
        {
            ObjectResult result = null;

            try
            {
                int id = _pService.AddPokemon(model);
                ItemResponse<int> response = new ItemResponse<int>() { Item = id };
                result = Created201(response);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                ErrorResponse response = new ErrorResponse(ex.Message);
                result = StatusCode(500, response);
            }
            return result;
        }

    }
}
