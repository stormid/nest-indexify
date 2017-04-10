namespace Nest.Indexify.Contributors.Mapping
{
    public class FromAttributesIndexMappingContributor<TType> : IndexMappingContributor<TType> where TType : class
    {
        private readonly int _maxRecursion;
        public FromAttributesIndexMappingContributor(int maxRecursion = 0, int order = 0) : base(order)
        {
            _maxRecursion = maxRecursion;
        }

        protected override TypeMappingDescriptor<TType> Mapping(TypeMappingDescriptor<TType> descriptor)
        {
            return descriptor.AutoMap(_maxRecursion);
        }
    }
}
