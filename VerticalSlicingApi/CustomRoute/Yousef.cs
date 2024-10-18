using System.Globalization;

namespace VerticalSlicingApi.CustomRoute
{
    public class Yousef:IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var val))
            {
                int vall = Convert.ToInt32(val) ;
                if (vall.GetType() == typeof(int))
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
