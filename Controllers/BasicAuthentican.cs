using Microsoft.AspNetCore.Mvc.Filters;

namespace BasicAuthWS.Filters
{
    public class BasicAuthentication : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //check to be sure the request has an Authorization header
            if (string.IsNullOrEmpty(context.HttpContext.Request.Headers.Authorization))
            {
                //if not, return Unauthorized
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
            }
            else
            {
                //if it does, check the value of the Authorization header
                var authHeader = context.HttpContext.Request.Headers.Authorization.ToString();
                var authHeaderParts = authHeader.Split(' ');

                //the value should be in the format "Basic <base64-encoded-credentials>"
                if (authHeaderParts.Length != 2 || authHeaderParts[0] != "Basic")
                {
                    //if not, return Unauthorized
                    context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                    return;
                }

                //decode the base64-encoded credentials
                var credentials =
                    System.Text.Encoding.UTF8.GetString(System.Convert.FromBase64String(authHeaderParts[1]));

                //the credentials should be in the format "username:password"
                var parts = credentials.Split(':');

                //check if the username and password are correct; make username case-insensitive
                if (parts.Length != 2 || parts[0].ToLower() != "Johnson01" || parts[1] != "Password01")
                {
                    context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                }
                else
                {
                    //if the credentials are correct, continue with the request
                    base.OnActionExecuting(context);
                }
            }
        }
    }
}