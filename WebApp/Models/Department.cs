namespace WebApp.Models
{
    public class Department : BaseEntity
    {
        public override long Id { get; set; }
        public override string Name { get; set; }
        public string Boss { get; set; }
        public long CompanyId { get; set; }
        public Company CompanyEntity;
        public Department() { }// { company = new Company(); }
        public Department(long companyId) { Id = 0; Name = ""; Boss = ""; CompanyId = companyId; }
        public Department(string name, string boss, long companyId) { Id = 0; Name = name; Boss = boss; CompanyId = companyId; }
        public override string ToString() { return "{Id = " + Id.ToString() + ", Name = " + Name + ", Boss = " + Boss + ", Company = " + CompanyEntity?.ToString() + "}"; }
    }

}
