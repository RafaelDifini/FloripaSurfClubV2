using System;

namespace FloripaSurfClubCore.Requests.Professor
{
    public class UpdateProfessorRequest 
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal ValorAReceber { get; set; }
    }
}
