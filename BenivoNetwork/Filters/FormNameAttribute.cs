using System.Reflection;
using System.Web.Mvc;

namespace BenivoNetwork.Filters
{
    public class FormNameAttribute : ActionMethodSelectorAttribute
    {
        private string _formName;

        public FormNameAttribute(string formName)
        {
            _formName = formName;
        }

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.HttpContext.Request["FormName"] == _formName;
        }
    }

}