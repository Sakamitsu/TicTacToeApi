using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicTacToeApi.Models;
using TicTacToeApi.Data;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using System.Numerics;

namespace TicTacToeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApiContext _context;

        public GameController(ApiContext context)
        {
            _context = context;        
        }

        [HttpPost]
        public JsonResult CreateEdit(Game game)
        {
            if (game.Id == 0)
            {
                _context.Games.Add(game);
            }
            else
            {
                var gameInDb = _context.Games.Find(game.Id);

                if (gameInDb == null)
                {
                    return new JsonResult(NotFound());
                }
                bool isXWon = game.Position.Contains("XXX") && (game.Position.IndexOf('X') == 0 || game.Position.IndexOf('X') == 3 || game.Position.IndexOf('X') == 6)
                              || new Regex(@"^.*X..X..X.*$", RegexOptions.Compiled).IsMatch(game.Position)
                              || new Regex(@"^X...X...X$", RegexOptions.Compiled).IsMatch(game.Position)
                              || new Regex(@"^..X.X.X..$", RegexOptions.Compiled).IsMatch(game.Position);

                bool isOWon = game.Position.Contains("OOO") && (game.Position.IndexOf('O') == 0 || game.Position.IndexOf('O') == 3 || game.Position.IndexOf('O') == 6)
                              || new Regex(@"^.*O..O..O.*$", RegexOptions.Compiled).IsMatch(game.Position)
                              || new Regex(@"^O...O...O$", RegexOptions.Compiled).IsMatch(game.Position)
                              || new Regex(@"^..O.O.O..$", RegexOptions.Compiled).IsMatch(game.Position);

                bool isDraw = !game.Position.Contains('?');
                
                gameInDb = game;
                
                if (isXWon) gameInDb.SetStatus(Status.XWon);
                else if (isOWon) gameInDb.SetStatus(Status.OWon);
                else if (isDraw) gameInDb.SetStatus(Status.Draw);
            }
            _context.SaveChanges();
            return new JsonResult(Ok(game));
        }

        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Games.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            return new JsonResult(Ok(result));
        }

        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Games.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }
            _context.Games.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            var result = _context.Games.ToList();

            return new JsonResult(Ok(result));
        }
    }
}
