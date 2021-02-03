using Autofac;
using System;

namespace AutofacTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<CmmOne>().Named<ICMM>("CmmOne").SingleInstance();
            builder.RegisterType<CmmSecond>().Named<ICMM>("CmmSecond").SingleInstance();
            builder.RegisterType<SubFirst>();
            builder.RegisterType<SubSecond>();
            //builder.Register(c => new SubSecond(c.ResolveNamed<ICMM>("CmmSecond")));
            builder.Register((c, p) =>
            {
                return new SubSecond(c.ResolveNamed<ICMM>(p.Named<string>("Sel")));
            });
            builder.Register(c => new SubFirst(c.Resolve<SubSecond>(new NamedParameter("Sel", "CmmOne")))).Named<SubFirst>("One");
            builder.Register(c => new SubFirst(c.Resolve<SubSecond>(new NamedParameter("Sel", "CmmSecond")))).Named<SubFirst>("Second");
            builder.Register((c, p) =>
            {
                if (p.Named<string>("Scl") == "1")
                    return new Root(c.ResolveNamed<SubFirst>("One"));
                else
                    return new Root(c.ResolveNamed<SubFirst>("Second"));
            });


            var container = builder.Build();

            var root = container.Resolve<Root>(new NamedParameter("Scl", "1"));
            var root1 = container.Resolve<Root>(new NamedParameter("Scl", "2"));
            root.Show();
            root1.Show();
            Console.ReadLine();
        }
    }
}
