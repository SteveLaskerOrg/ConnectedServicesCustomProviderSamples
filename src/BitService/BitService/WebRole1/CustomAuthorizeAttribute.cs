using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Data.Linq;

// Currently this is not used. It could in theory be used to implement asset-based security, but that is left as an exercise
// to the reader.

namespace WebRole1
{
    public class CustomAuthorizeAttribute : System.Web.Http.Filters.AuthorizationFilterAttribute //AuthorizeAttribute
    {
        // TODO: make sure we do the caching correctly here.
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            //base.OnAuthorization(actionContext);
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            // This method has the job of determining if the user who is attempting to 
            // authorize has access to the resource they are trying to access.

            // We need 1) to get the user and 2) get the resource and owner
            // and 3) compare the user to the owner of the resource

            string userName = actionContext.ControllerContext.RequestContext.Principal.Identity.Name;

            // This code requires that UserId be a route parameter.
            int userId = -1;
            if (actionContext.ControllerContext.RouteData.Values.ContainsKey("UserId"))
            {
                userId = Convert.ToInt32(actionContext.ControllerContext.RouteData.Values["UserId"]);
            }

            // Check the database to see if the UserId from the Route Data matches the
            // Name
            
        }
    }
}