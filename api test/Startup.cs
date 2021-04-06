using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using api_test.MyClass;

[assembly: OwinStartup(typeof(api_test.Startup))]

namespace api_test
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            apicall.start();
        }
    }
}
