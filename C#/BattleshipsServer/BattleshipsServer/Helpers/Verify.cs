using System;

namespace BattleshipsServer.Helpers
{
    public static class Verify
    {
        public static void NotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"{name} was null.");
            }
        }
    }
}
