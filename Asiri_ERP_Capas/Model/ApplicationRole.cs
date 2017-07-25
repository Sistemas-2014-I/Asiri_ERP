using Microsoft.AspNet.Identity.EntityFramework;

namespace Model
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }

        //Agregar campos con migraciones
        public string Description { get; set; }


        

    }
}
