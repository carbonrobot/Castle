using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Domain;
using Castle.Services.Logging;
using Castle.Services.Providers;

namespace Castle.Services
{
    public abstract class DataService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataService"/> class.
        /// </summary>
        /// <param name="context">The repository data context.</param>
        protected DataService(IDataContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Executes a given function while isolating exception handling
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="func">The method to execute</param>
        protected ServiceResponse<T> Execute<T>(Func<T> func)
        {
            var response = new ServiceResponse<T>();
            try
            {
                response.Result = func.Invoke();
                response.HasError = false;
                response.Exception = null;
            }
            catch (Exception ex)
            {
                this.Log().Error(() => ex.ToString());

                response.Result = default(T);
                response.HasError = true;
                response.Exception = ex;
            }
            return response;
        }

        /// <summary>
        /// Executes a given action while isolating exception handling
        /// </summary>
        /// <typeparam name="T">The type of the result</typeparam>
        /// <param name="action">The method to execute</param>
        protected ServiceResponse Execute(Action action)
        {
            var response = new ServiceResponse();
            try
            {
                action.Invoke();
                response.HasError = false;
                response.Exception = null;
            }
            catch (Exception ex)
            {
                this.Log().Error(() => ex.ToString());

                response.HasError = true;
                response.Exception = ex;
            }
            return response;
        }

        private IDataContext Context;
    }
}