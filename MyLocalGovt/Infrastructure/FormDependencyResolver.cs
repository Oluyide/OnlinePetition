
//using Autofac.Integration.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Web;
//using System.Web.Mvc;
//using BusinessLogic.Helper;
//using BusinessLogic.Interfaces;
//using BusinessLogic.Utility;
//using System.Web.ApplicationServices;
//using BusinessLogic.Services;
//using Autofac;
//using MyLocalGovt;
//using BusinessLayer.Infrastructures.Fakes;


//namespace MyLocalGovt.Infrastucture
//{
//    public class FormDependencyResolver
//    {
//        public static void RegisterDependencies()
//        {
//             //AutofacDependencyResolver.Current.ApplicationContainer
//            var builder = new ContainerBuilder();
//            //Register All Controllers
//            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
//            builder.RegisterControllers(typeof(MvcApplication).Assembly);

//            //or be explicit
//            //HTTP context and other related stuff
//            //HTTP context and other related stuff
//            builder.Register(c =>
//                //register FakeHttpContext when HttpContext is not available
//                HttpContext.Current != null ?
//                (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
//                (new FakeHttpContext("~/") as HttpContextBase))
//                .As<HttpContextBase>()
//                .InstancePerRequest();
//            builder.Register(c => c.Resolve<HttpContextBase>().Request)
//                .As<HttpRequestBase>().InstancePerRequest();

//            builder.Register(c => c.Resolve<HttpContextBase>().Response)
//                .As<HttpResponseBase>()
//                .InstancePerRequest();
//            builder.Register(c => c.Resolve<HttpContextBase>().Server)
//                .As<HttpServerUtilityBase>()
//                .InstancePerRequest();
//            builder.Register(c => c.Resolve<HttpContextBase>().Session)
//                .As<HttpSessionStateBase>()
//                .InstancePerRequest();
//            //The requested service 'NHibernate.ISession' has not been registered. To avoid this exception, either register a component to provide the service, check for service registration using IsRegistered(), or use the ResolveOptional() method to resolve an optional dependency.


//            builder.RegisterGeneric(typeof(NHibernateRepository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
//           builder.RegisterType<UsersInfo>().As<IUsersInfo>().InstancePerRequest();
//            //builder.RegisterGeneric(typeof(Session()).As(typeof(ISession<,>)).InstancePerLifetimeScope();
//            //builder.RegisterType<AsyncService>().As<IAsyncService>().InstancePerLifetimeScope();//.InstancePerLifetimeScope();
//            //builder.RegisterType<SMSSender>().As<ISMSSender>().InstancePerRequest();
//            //builder.RegisterType<SMSAccount>().As<ISMSAccount>().InstancePerRequest();
//            //builder.RegisterType<BusinessLogic.Services.AuthenticationService>().As<IAuthenticationService>().InstancePerRequest();
//            //builder.RegisterType<PageHelper>().As<IPageHelper>().InstancePerRequest();
//            //builder.RegisterType<EmailSender>().As<IEmailSender>().InstancePerRequest();
//            //builder.RegisterType<EmailAccount>().As<IEmailAccount>().InstancePerRequest();
//            //builder.RegisterType<UtilityService>().As<IUtilityService>().InstancePerRequest();
//            //builder.RegisterType<Session>().As<ISession>().InstancePerRequest();
//            //builder.RegisterType<WebWorker>().As<IWebWorker>().InstancePerRequest();
//            //builder.RegisterType<DateTimeHelper>().As<IDateTimeHelper>().InstancePerRequest();
//            //builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
//            //builder.RegisterType<ProjectServices>().As<IProjects>().InstancePerRequest();
//            //builder.RegisterType<GeneralService>().As<IGeneralService>().InstancePerRequest();
//            //builder.RegisterType<SmsService>().As<ISmsService>().InstancePerRequest();
//            builder.RegisterType<HtmlHelper>().InstancePerDependency();
           

//            IContainer container = builder.Build();
//            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

//        }
//    }
//}