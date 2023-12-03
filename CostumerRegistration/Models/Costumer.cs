using CostumerRegistration.Services.Enums;
using CostumerRegistration.Services.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;


namespace CostumerRegistration.Models
{
    public class Costumer
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Nome { get; set; }
        [CnpjValidation(ErrorMessage = "O CNPJ é inválido.")]
        public long Cnpj { get; set; }
        public SegmentEnum Segment { get; set; }
        public int Cep { get; set; }
        public string Street { get; set; }
        public string? Complement { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Number { get; set; }
        public string? Photo { get; set; }
       
    }
}
