using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Harmony;
using InjectionModule;
using TestService;

namespace Bootstrap
{
    public class Program
    {
        
        private static void Main(string[] args)
        {
            var folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filename = Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            var targetExe = Directory.EnumerateFiles(folder)
                .Single(x => x.EndsWith(".exe") && Path.GetFileName(x) != filename && !Path.GetFileName(x).Contains("EasyHook"));


            var context = new InjectorContext(args, HarmonyInstance.Create("bootstrapper"));
            var injectors = new Injector[]
            {
                new ScimControllerInjector(context),
                new EventLogInjector(context)
            };
            foreach (var injector in injectors) injector.Install();


            var serviceAsm = Assembly.LoadFile(targetExe);
            var entryPoint = serviceAsm.EntryPoint;

            if (entryPoint.GetParameters().Any())
                Task.Run(() => entryPoint.Invoke(null, new[] {new string[0]}));
            else
                Task.Run(() => entryPoint.Invoke(null, null));
            Console.WriteLine("Press CTRL+C to Shutdown...");
            ApplicationLifecycle.ShutdownCompleteHandle.WaitOne();
        }
    }
}