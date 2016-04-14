using System.Web.Mvc;

namespace SmartAdminMvc.Areas.ContosoUniversity
{
    public class ContosoUniversityAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ContosoUniversity";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ContosoUniversity_default",
                "ContosoUniversity/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}