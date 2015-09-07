using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("Nest.Indexify")]
[assembly: AssemblyDescription("Helper library for Elasticsearch index creation using a contributor model")]
[assembly: AssemblyCompany("Storm ID Ltd")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
    [assembly: AssemblyConfiguration("Release")]
#endif

[assembly: AssemblyProduct("Nest.Queryify")]
[assembly: ComVisible(false)]
