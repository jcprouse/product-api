using System;

namespace ProductApi.Api.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entity) : base(GetFormattedMessage(entity), null)
        {
        }
        
        private static string GetFormattedMessage(string entity) => entity + " not found";
    }
}