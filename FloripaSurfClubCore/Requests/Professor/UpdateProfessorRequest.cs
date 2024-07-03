using System;

namespace FloripaSurfClubCore.Requests.Professor
{
    public class UpdateProfessorRequest : Request
    {
        public string Nome { get; set; }
        public decimal ValorAReceber { get; set; }
    }
}
