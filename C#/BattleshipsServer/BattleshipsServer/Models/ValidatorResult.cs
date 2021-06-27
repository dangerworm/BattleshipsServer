using System.Collections.Generic;
using System.Linq;

namespace BattleshipsServer.Models
{
    public class ValidatorResult
    {
        public IList<string> Errors { get; set; }
        public bool IsValid => !Errors.Any();

        public ValidatorResult()
        {
            Errors = new List<string>();
        }
    }
}
