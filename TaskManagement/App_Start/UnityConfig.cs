using DataAccess.Components.Interface;
using DataAccess.Components.Repository;
using DataAccess.Components.Services;
using DataAccess.DataContext;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace TaskManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ITaskService, TaskService>();
            container.RegisterType<ITaskRepository, TaskRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}