namespace Qwirkle.WebApi.Server.Controllers;

[ApiController]
//[Authorize]
[Route("Game")]
public class GameController : ControllerBase
{
    private readonly ILogger<GameController> _logger;
    private readonly CoreService _coreService;
    private readonly InfoService _infoService;
    private readonly UserManager<UserDao> _userManager;
    private int UserId => int.Parse(_userManager.GetUserId(User) ?? "0");

    public GameController(CoreService coreService, InfoService infoService, UserManager<UserDao> userManager, ILogger<GameController> logger)
    {
        _logger = logger;
        _coreService = coreService;
        _infoService = infoService;
        _userManager = userManager;
    }

    [HttpPost("New")]
    public async Task<ActionResult> CreateGameAsync(OpponentsModel opponentsModel)
    {
        var opponentsNames = new HashSet<string> { opponentsModel.Opponent1, opponentsModel.Opponent2, opponentsModel.Opponent3 };
        var usersIdsList = new List<int> { UserId };
        usersIdsList.AddRange(opponentsNames.Select(opponentName => _infoService.GetUserId(opponentName)));
        usersIdsList.RemoveAll(id => id == 0);
        var usersIds = new HashSet<int>(usersIdsList);
        if (!usersIds.Contains(UserId)) return new BadRequestObjectResult("user not in the game");
        var gameId = await _coreService.CreateGameAsync(usersIds);
        return new ObjectResult(gameId);
    }


    [HttpGet("{gameId:int}")]
    public ActionResult GetGame(int gameId)
    {
        var game = _infoService.GetGameWithTilesOnlyForAuthenticatedUser(gameId, UserId);
        return game is null ? StatusCode(StatusCodes.Status404NotFound) : new ObjectResult(game);
    }


    [HttpGet("UserGamesIds")]
    public ActionResult GetUserGamesIds() => new ObjectResult(_infoService.GetUserGames(UserId));
}
