using HeyListen.Database;
using HeyListen.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace HeyListen.Web.Controllers;

[ApiController]
[Route("games")]
public class GameController : ControllerBase
{
    private readonly GameRepository _gameRepository;

    public GameController(GameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Game>> Get()
    {
        return await _gameRepository.GetAll();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<Game> Get(Guid id)
    {
        return await _gameRepository.GetById(id);
    }
}