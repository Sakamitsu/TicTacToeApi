using System.ComponentModel.DataAnnotations;

namespace TicTacToeApi.Models
{
    public class Game
    {
        public int Id { get; set; }
        [RegularExpression(@"^[XO?]{9}$")]
        public string Position { get; set; } = "?????????";
        public Status Status { get; private set; } = Status.InProgress;
        
        public void SetStatus(Status status)
        {
            Status = status;
        }
    }


    public enum Status
    {
        InProgress,
        Draw,
        XWon,
        OWon
    }
}

