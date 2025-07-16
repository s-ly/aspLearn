using System.Collections.Generic;
namespace PartyInvites.Models
{
    public static class Repository
    {
        private static List<GuestRsponse> responses = new List<GuestRsponse>();
        public static IEnumerable<GuestRsponse> Responses => responses;
        public static void AddResponse(GuestRsponse response)
        {
            responses.Add(response);
        }
    }
}