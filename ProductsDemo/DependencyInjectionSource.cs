using System;
using System.Windows.Markup;

namespace ProductsDemo;

public class DependencyInjectionSource : MarkupExtension {
    public static Func<Type, object, string, object?>? Resolver { get; set; }

    public Type Type { get; set; }
    public object Key { get; set; }
    public string Name { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider) => Resolver(Type, Key, Name);
}