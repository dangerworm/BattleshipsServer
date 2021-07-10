using BattleshipsServer.Models;

namespace BattleshipsServer.Processors
{
    public class ProcessorResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ValidatorResult ValidatorResult { get; set; }
    }
}
