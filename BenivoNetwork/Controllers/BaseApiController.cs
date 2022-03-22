using BenivoNetwork.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;

namespace BenivoNetwork.Controllers
{
    [Authorize]
    public class BaseApiController : ApiController
    {
        protected IHttpActionResult Success(string message = default)
        {
            return Ok(new ResponseModel(true, message));
        }

        protected IHttpActionResult Success<T>(T data, string message = default)
        {
            return Ok(new ResponseModel<T>(true, message, data));
        }

        protected IHttpActionResult Error(string message = default)
        {
            return Ok(new ResponseModel(false, message));
        }

        protected IHttpActionResult Error(Exception ex)
        {
            return Ok(new ResponseModel(false, HttpContext.Current.IsDebuggingEnabled ? ex.GetBaseException().Message : "Something went wrong."));
        }

        protected string ModelErrors
        {
            get
            {
                var errors = new List<string>();

                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                return string.Join("\n", errors);
            }
        }
    }
}
