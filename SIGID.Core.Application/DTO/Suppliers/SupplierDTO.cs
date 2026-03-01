namespace SIGID.Core.Application.DTO.Suppliers
{
    public class SupplierDTO
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string Rnc { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }

    public class CreateSupplierDTO
    {
        public string CompanyName { get; set; }
        public string Rnc { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }

    public class UpdateSupplierDTO
    {
        public string CompanyName { get; set; }
        public string Rnc { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
    }
}
