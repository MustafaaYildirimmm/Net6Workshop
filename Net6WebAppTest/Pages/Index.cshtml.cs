using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Net6WebAppTest.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IStringLocalizer _localizer;
        private readonly IStringLocalizer _localizer2;

        public IndexModel(ILogger<IndexModel> logger, IStringLocalizerFactory factory)
        {
            _logger = logger;
            //_localizer = factory.Create("IndexPage", System.Reflection.Assembly.GetExecutingAssembly().GetName().Name);

            //var test = _localizer.GetString("test");
            //var testLAng = _localizer[LanguageKey.Asd].Value;

            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create(type);
            _localizer2 = factory.Create("SharedResource", assemblyName.Name);

            var testLAng = _localizer[LanguageKey.Test].Value;
            var testLAng2 = _localizer2[LanguageKey.Test].Value;

            System.Resources.ResourceManager rsMgr = new System.Resources.ResourceManager("SharedResources", Assembly.GetAssembly(type));


        }

        public void OnGet()
        {


        }

    }
}